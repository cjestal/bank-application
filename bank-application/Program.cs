namespace bank_application
{
    // Main entry point of this application
    class Program
    {
        static void Main(string[] args)
        {
            AccountList accountList = GenerateInitialAccounts(10); // make an account list with 5 accounts. 5 new accounts allowed.
            bool appRunning = true;

            while (appRunning)
            {
                Console.WriteLine("Welcome to the Bank Application");
                Console.WriteLine("1. Create an account");
                Console.WriteLine("2. Make a deposit");
                Console.WriteLine("3. Make a withdrawal");
                Console.WriteLine("4. Check balance");
                Console.WriteLine("5. View transaction history");
                Console.WriteLine("6. Exit");
                Console.Write("\nPlease choose an option:");
                string userChoice = Console.ReadLine();

                Console.Clear(); // Clear the console when a user chooses a step

                switch (userChoice)
                {
                    case "1":
                        CreateAccount(accountList);
                        break;
                    case "2":
                        Account depositAccount = SelectUserAccount(accountList);
                        if (depositAccount != null) MakeDeposit(depositAccount);
                        break;
                    case "3":
                        Account withdrawalAccount = SelectUserAccount(accountList);
                        if (withdrawalAccount != null) MakeWithdrawal(withdrawalAccount);
                        break;
                    case "4":
                        Account balanceAccount = SelectUserAccount(accountList);
                        if (balanceAccount != null) CheckBalance(balanceAccount);
                        break;
                    case "5":
                        Account transactionHistoryAccount = SelectUserAccount(accountList);
                        if (transactionHistoryAccount != null) ViewTransactionHistory(transactionHistoryAccount);
                        break;
                    case "6":
                        Console.WriteLine("Exiting application.");
                        appRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }


        private static AccountList GenerateInitialAccounts(int size) // Added size parameter
        {
            AccountList accountList = new AccountList(size); // Using the size parameter

            int accountsToGenerate = size / 2; // generate only half to allow account creation
            for (int i = 1; i <= accountsToGenerate; i++)
            {
                string name = $"BankUser{i}";
                decimal initialBalance = 100 * i; // Example initial balance
                Account newAccount = new Account(name, initialBalance);
                accountList.AddAccount(newAccount);
            }
            return accountList;
        }

        private static void CreateAccount(AccountList accountList)
        {
            Console.Clear();
            Console.WriteLine("Enter your name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter initial balance:");
            decimal initialBalance = Convert.ToDecimal(Console.ReadLine());
            Account newAccount = new Account(name, initialBalance);
            accountList.AddAccount(newAccount);
            Console.WriteLine($"Account created for {name} with balance {initialBalance}");
            Console.WriteLine();
        }

        private static void MakeDeposit(Account account)
        {
            if (account == null) return;
            Console.Clear();
            Console.Write("Enter deposit amount:");
            decimal depositAmount = Convert.ToDecimal(Console.ReadLine());
            account.MakeDeposit(depositAmount, DateTime.Now, "Deposit");
            Console.WriteLine($"{account.Owner} deposited {depositAmount}. New balance: {account.Balance}");
            Console.WriteLine();
        }

        private static void MakeWithdrawal(Account account)
        {
            // Check if account is null to avoid NullReferenceException
            if (account == null) return;

            // Clear the console for better readability
            Console.Clear();

            // Prompt user to enter the withdrawal amount
            Console.Write("Enter withdrawal amount:");
            decimal withdrawalAmount = Convert.ToDecimal(Console.ReadLine());

            try
            {
                // Attempt to make a withdrawal from the account
                account.MakeWithdrawal(withdrawalAmount, DateTime.Now, "Withdrawal");

                // Inform the user of a successful withdrawal and display the remaining balance
                Console.WriteLine($"{account.Owner} withdrew {withdrawalAmount}. Remaining balance: {account.Balance}");
                Console.WriteLine();
            }
            catch (InvalidOperationException ex)
            {
                // Catch any InvalidOperationExceptions (e.g., insufficient funds) and display the error message to the user
                Console.WriteLine($"Withdrawal failed: {ex.Message}");
                Console.WriteLine();
            }
        }

        private static void CheckBalance(Account account)
        {
            if (account == null) return;
            Console.Clear();
            Console.WriteLine($"Current balance: {account.Balance}");
            Console.WriteLine();
        }

        private static void ViewTransactionHistory(Account account)
        {
            if (account == null) return; // Check if the account object is null to prevent errors

            Console.Clear();

            var transactions = account.GetTransactionHistory(); // Retrieve the transaction history from the account

            Console.WriteLine($"Transaction history for {account.Owner}:"); // Display the account owner's transaction history header

            // Iterate through each transaction and display its details
            foreach (var transaction in transactions)
            {
                Console.WriteLine($"{transaction.Date.ToShortDateString()} - {transaction.Notes}: {transaction.Amount}");
            }

            Console.WriteLine();
        }

        private static Account SelectUserAccount(AccountList accountList)
        {
            Console.WriteLine("Select an account by index:");
            // Iterate through all accounts in the list
            for (int i = 0; i < accountList.GetCount(); i++)
            {
                Account account = accountList.GetAccount(i); // Retrieve account at current index
                if (account != null) // Check if account exists
                {
                    Console.WriteLine($"{i}. Account Number: {account.Number}, Owner: {account.Owner}");
                }
            }

            int index;
            // Attempt to parse user input as an integer for account selection
            if (int.TryParse(Console.ReadLine(), out index))
            {
                Account selectedAccount = accountList.GetAccount(index); // Retrieve selected account
                if (selectedAccount == null) // Check if selection is valid
                {
                    Console.WriteLine("Invalid account selection."); // Inform user of invalid selection
                    return null; // Return null to indicate failure
                }
                return selectedAccount; // Return the successfully selected account
            }
            else
            {
                Console.WriteLine("Invalid input."); // Inform user of invalid input
                return null; // Return null to indicate failure
            }
        }

    }
}
