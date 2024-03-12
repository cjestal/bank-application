namespace bank_application
{
    public class Deposit : Transaction
    {
        public Deposit(decimal amount, DateTime date, string notes, Account account) : base(amount, date, notes, account)
        {
        }
    }
}