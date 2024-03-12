using System;

namespace bank_application
{
    public class AccountList
    {
        private Account[] accounts;
        private int count;

        public AccountList(int size)
        {
            accounts = new Account[size];
            count = 0;
        }

        public void AddAccount(Account account)
        {
            if (count >= accounts.Length)
            {
                Console.WriteLine("Account list is full. Cannot add more accounts.");
                return;
            }
            accounts[count] = account;
            count++;
        }

        public Account GetAccount(int index)
        {
            if (index < 0 || index >= count)
            {
                Console.WriteLine("Invalid account index.");
                return null;
            }
            return accounts[index];
        }

        public bool DeleteAccount(string accountNumber)
        {
            for (int i = 0; i < count; i++)
            {
                if (accounts[i].Number == accountNumber)
                {
                    // Shift all accounts one position to the left to fill the gap
                    for (int j = i; j < count - 1; j++)
                    {
                        accounts[j] = accounts[j + 1];
                    }
                    accounts[count - 1] = null; // Nullify the last element
                    count--; // Decrease the count of accounts
                    return true; // Account deleted successfully
                }
            }
            return false; // Account not found
        }
    }
}
