namespace bank_application
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Bank Application");
            Account userAccount = new Account("Default User", 0);
            bool appRunning = true;

            while (appRunning)
            {
                Console.WriteLine("\nPlease choose an option:");
                Console.WriteLine("1. Create an account");
                Console.WriteLine("2. Make a deposit");
                Console.WriteLine("3. Make a withdrawal");
                Console.WriteLine("4. Check balance");
                Console.WriteLine("5. Exit");

                string userChoice = Console.ReadLine();

                switch (userChoice)
                {
                    case "1":
                        CreateAccount(ref userAccount);
                        break;
                    case "2":
                        MakeDeposit(userAccount);
                        break;
                    case "3":
                        MakeWithdrawal(userAccount);
                        break;
                    case "4":
                        CheckBalance(userAccount);
                        break;
                    case "5":
                        ExitApplication(ref appRunning);
                        break;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }

        private static void CreateAccount(ref Account userAccount)
        {
            Console.WriteLine("Enter your name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter initial balance:");
            decimal initialBalance = Convert.ToDecimal(Console.ReadLine());
            userAccount = new Account(name, initialBalance);
            Console.WriteLine($"Account created for {name} with balance {initialBalance}");
        }

        private static void MakeDeposit(Account userAccount)
        {
            Console.WriteLine("Enter deposit amount:");
            decimal depositAmount = Convert.ToDecimal(Console.ReadLine());
            userAccount.MakeDeposit(depositAmount, DateTime.Now, "Deposit");
            Console.WriteLine($"Deposited {depositAmount}");
        }

        private static void MakeWithdrawal(Account userAccount)
        {
            Console.WriteLine("Enter withdrawal amount:");
            decimal withdrawalAmount = Convert.ToDecimal(Console.ReadLine());
            try
            {
                userAccount.MakeWithdrawal(withdrawalAmount, DateTime.Now, "Withdrawal");
                Console.WriteLine($"Withdrew {withdrawalAmount}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Withdrawal failed: {ex.Message}");
            }
        }

        private static void CheckBalance(Account userAccount)
        {
            Console.WriteLine($"Current balance: {userAccount.Balance}");
        }

        private static void ExitApplication(ref bool appRunning)
        {
            Console.WriteLine("Exiting application.");
            appRunning = false;
        }
    }
}
