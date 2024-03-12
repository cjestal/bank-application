namespace bank_application
{
    public class Deposit : Transaction
    {
        public Deposit(decimal amount, DateTime date, Account account) : base(amount, date, "deposit", account)
        {
        }
    }
}