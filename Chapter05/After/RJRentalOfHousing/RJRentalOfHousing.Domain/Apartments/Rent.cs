using RJRentalOfHousing.Domain.Shared;

namespace RJRentalOfHousing.Domain.Apartments
{
    public record Rent : Money
    {
        protected Rent() { }

        private Rent(decimal amount, string currencyCode, ICurrencyLookup currencyLookup) : base(amount, currencyCode, currencyLookup)
        {
            if (amount < 0)
                throw new ArgumentException("租金不能为负数", nameof(amount));
        }

        internal Rent(decimal amount, string currencyCode) : base(amount, new CurrencyDetails { CurrencyCode = currencyCode }) { }

        public static Rent FromDecimal(decimal amount, string currency, ICurrencyLookup currencyLookup) => new Rent(amount, currency, currencyLookup);

        public static Rent NoRent = new Rent();
    }
}
