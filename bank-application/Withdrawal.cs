namespace bank_application
{
    public class Withdrawal : Transaction
    {
        public Withdrawal(decimal amount, DateTime date, string notes, Account account) : base(amount, date, notes, account)
        {
        }
    }
}
