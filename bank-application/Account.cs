using System;

namespace bank_application
{
    public enum TransactionType
    {
        Deposit,
        Withdrawal
    }

    public class Account
    {
        public string Number { get; }
        public string Owner { get; set; }
        public decimal Balance
        {
            get
            {
                decimal balance = 0;
                foreach (var transaction in allTransactions)
                {
                    if (transaction != null) // Check for null since arrays can have uninitialized elements
                    {
                        balance += transaction.Amount;
                    }
                }
                return balance;
            }
        }

        private static int accountNumberSeed = 1234567890;
        private Transaction[] allTransactions = new Transaction[100]; // Assuming a fixed size for simplicity
        private int transactionCount = 0; // Keep track of the number of transactions

        public Account(string name, decimal initialBalance)
        {
            this.Owner = name;
            this.Number = accountNumberSeed.ToString();
            accountNumberSeed++;

            if (initialBalance > 0)
            {
                MakeTransaction(initialBalance, DateTime.Now, "Initial balance", TransactionType.Deposit);
            }
        }

        public void MakeDeposit(decimal amount, DateTime date, string note)
        {
            MakeTransaction(amount, date, note, TransactionType.Deposit);
        }

        public void MakeWithdrawal(decimal amount, DateTime date, string note)
        {
            if (Balance - amount < 0)
            {
                throw new InvalidOperationException("Not sufficient funds for this withdrawal");
            }
            MakeTransaction(amount, date, note, TransactionType.Withdrawal);
        }

        private void MakeTransaction(decimal amount, DateTime date, string note, TransactionType type)
        {
            // check for invalid amount
            if (amount <= 0)
            {
                Console.WriteLine("Error: Amount must be positive.");
                return;
            }

            // check for transaction limit
            if (transactionCount >= allTransactions.Length)
            {
                Console.WriteLine("Error: Transaction limit reached.");
                return;
            }
            Transaction transaction = null;
            switch (type)
            {
                case TransactionType.Deposit:
                    transaction = new Deposit(amount, date, this);
                    break;
                case TransactionType.Withdrawal:
                    transaction = new Withdrawal(amount, date, this);
                    break;
                default:
                    Console.WriteLine("Error: Unsupported transaction type.");
                    return;
            }
            if (transaction != null)
            {
                allTransactions[transactionCount] = transaction;
                transactionCount++;
            }
        }

        public Transaction[] GetTransactionHistory()
        {
            return allTransactions;
        }
    }
}

