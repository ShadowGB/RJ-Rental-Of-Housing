using RJRentalOfHousing.Domain.Shared;

namespace RJRentalOfHousing.Domain.Apartments
{
    public record Deposit : Money
    {
        protected Deposit() { }

        private Deposit(decimal amount, string currencyCode, ICurrencyLookup currencyLookup) : base(amount, currencyCode, currencyLookup)
        {
            if (amount < 0)
                throw new ArgumentException("押金不能为负数", nameof(amount));
        }

        internal Deposit(decimal amount, string currencyCode) : base(amount, new CurrencyDetails { CurrencyCode = currencyCode }) { }

        public static Deposit FromDecimal(decimal amount, string currency, ICurrencyLookup currencyLookup) => new Deposit(amount, currency, currencyLookup);

        public static Deposit NoDeposit = new Deposit();
    }
}
