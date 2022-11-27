using RJRentalOfHousing.Domain.Shared;

namespace RJRentalOfHousing.Infrastructure
{
    public class FixedCurrencyLookup : ICurrencyLookup
    {
        private static readonly IEnumerable<CurrencyDetails> _currencies =
            new[]
            {
                new CurrencyDetails
                {
                    CurrencyCode = "CNY",
                    DecimalPlaces = 2,
                    InUse = true
                },
                new CurrencyDetails
                {
                    CurrencyCode = "EUR",
                    DecimalPlaces = 2,
                    InUse = true
                },
                new CurrencyDetails
                {
                    CurrencyCode = "USD",
                    DecimalPlaces = 2,
                    InUse = true
                }
            };

        public CurrencyDetails FindCurrency(string currencyCode)
        {
            var currency = _currencies.FirstOrDefault(x => x.CurrencyCode == currencyCode);
            return currency ?? CurrencyDetails.None;
        }
    }
}
