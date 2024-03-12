namespace bank_application
{
    public class Withdrawal : Transaction
    {
        public Withdrawal(decimal amount, DateTime date, Account account) : base(amount, date, "withdrawal", account)
        {
        }
    }
}
