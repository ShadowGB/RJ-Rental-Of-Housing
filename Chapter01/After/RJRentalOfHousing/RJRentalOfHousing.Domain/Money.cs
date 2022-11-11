namespace RJRentalOfHousing.Domain
{
    public record Money
    {
        private const string DefaultCurrency = "CNY";

        public decimal Amount { get; internal set; }

        public string CurrencyCode { get; internal set; }

        public static Money FromString(string amount) => new Money(decimal.Parse(amount));

        public static Money FromDecamal(decimal amonut) => new Money(amonut);

        protected Money(decimal amount)
        {
            if (decimal.Round(amount, 2) != amount)
                throw new ArgumentOutOfRangeException(nameof(amount), "金额不能超过两位小数");
            Amount = amount;
        }

        public Money Add(Money m) => new Money(Amount + m.Amount);

        public Money Subtract(Money m) => new Money(Amount - m.Amount);

        public static Money operator +(Money m1, Money m2) => m1.Add(m2);

        public static Money operator -(Money m1, Money m2) => m1.Subtract(m2);
    }
}
