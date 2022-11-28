namespace RJRentalOfHousing.Domain
{
    public record Money
    {
        protected Money() { }

        private const string DefaultCurrency = "CNY";

        public decimal Amount { get; internal set; }

        public CurrencyDetails Currency { get; internal set; }

        public static Money FromString(string amount, string currency, ICurrencyLookup currencyLookup) => new Money(decimal.Parse(amount), currency, currencyLookup);

        public static Money FromDecimal(decimal amonut, string currency, ICurrencyLookup currencyLookup) => new Money(amonut, currency, currencyLookup);

        protected Money(decimal amount, string currencyCode, ICurrencyLookup currencyLookup)
        {
            if (string.IsNullOrEmpty(currencyCode))
                throw new ArgumentNullException(nameof(currencyCode), "币种编码不能为空");
            var currency = currencyLookup.FindCurrency(currencyCode);
            if (!currency.InUse)
                throw new ArgumentException($"非法的币种{currency.CurrencyCode}");
            if (decimal.Round(amount, currency.DecimalPlaces) != amount)
                throw new ArgumentOutOfRangeException(nameof(amount), $"币种{currency.CurrencyCode}最多只能有{currency.DecimalPlaces}位小数");
            Amount = amount;
            Currency = currency;
        }

        protected Money(decimal amount, CurrencyDetails currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public Money Add(Money m)
        {
            if (Currency != m.Currency)
                throw new CurrencyMismatchException("不能对两个不同币种的面值进行相加");
            return new Money(Amount + m.Amount, Currency);
        }

        public Money Subtract(Money m)
        {
            if (Currency != m.Currency)
                throw new CurrencyMismatchException("不能对两个不同币种的面值进行相减");
            return new Money(Amount - m.Amount, Currency);
        }

        public static Money operator +(Money m1, Money m2) => m1.Add(m2);

        public static Money operator -(Money m1, Money m2) => m1.Subtract(m2);
    }

    public class CurrencyMismatchException : Exception
    {
        public CurrencyMismatchException(string? message) : base(message)
        {
        }
    }
}
