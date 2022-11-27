using RJRentalOfHousing.Domain.Shared;

namespace RJRentalOfHousing.Tests
{
    public class FakeCurrencyLookup : ICurrencyLookup
    {
        private readonly IEnumerable<CurrencyDetails> _currencies =
            new[]
            {
                new CurrencyDetails(){CurrencyCode="CNY",DecimalPlaces=2,InUse=true},
                new CurrencyDetails(){CurrencyCode="JPY",DecimalPlaces=0,InUse=true},
                new CurrencyDetails(){CurrencyCode="USD",DecimalPlaces=2,InUse=true},
                new CurrencyDetails(){CurrencyCode="EUR",DecimalPlaces=2,InUse=true},
                new CurrencyDetails(){CurrencyCode="DEM",DecimalPlaces=2,InUse=false},
            };

        public CurrencyDetails FindCurrency(string currencyCode)
        {
            var currency = _currencies.FirstOrDefault(x=>x.CurrencyCode == currencyCode);
            return currency ?? CurrencyDetails.None;
        }
    }
}
