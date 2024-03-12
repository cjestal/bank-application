namespace bank_application
{
    public abstract class Transaction
    {
        public decimal Amount { get; }
        public DateTime Date { get; }
        public string Notes { get; }
        public Account Account { get; }

        protected Transaction(decimal amount, DateTime date, string notes, Account account)
        {
            Amount = amount;
            Date = date;
            Notes = notes;
            Account = account;
        }
    }
}
