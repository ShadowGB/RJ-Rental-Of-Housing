using RJRentalOfHousing.Domain;

namespace RJRentalOfHousing.Tests
{
    public class Money_Spec
    {
        private static readonly ICurrencyLookup CurrencyLookup = new FakeCurrencyLookup();

        [Fact]
        public void Two_of_same_amount_should_be_equal()
        {
            var m1 = Money.FromDecimal(5, "CNY", CurrencyLookup);
            var m2 = Money.FromDecimal(5, "CNY", CurrencyLookup);
            Assert.Equal(m1, m2);
        }

        [Fact]
        public void Two_of_same_amount_but_differentCurrencier_should_not_be_equal()
        {
            var m1 = Money.FromDecimal(5, "CNY", CurrencyLookup);
            var m2 = Money.FromDecimal(5, "USD", CurrencyLookup);
            Assert.NotEqual(m1, m2);
        }

        [Fact]
        public void FromString_and_FromDecimal_should_be_equal()
        {
            var m1 = Money.FromDecimal(5, "CNY", CurrencyLookup);
            var m2 = Money.FromString("5.00", "CNY", CurrencyLookup);
            Assert.Equal(m1, m2);
        }

        [Fact]
        public void Sum_of_money_gives_full_amount()
        {
            var coin1 = Money.FromDecimal(1, "CNY", CurrencyLookup);
            var coin2 = Money.FromDecimal(2, "CNY", CurrencyLookup);
            var coin3 = Money.FromDecimal(3, "CNY", CurrencyLookup);
            var banknote = Money.FromDecimal(6, "CNY", CurrencyLookup);
            Assert.Equal(banknote, coin1 + coin2 + coin3);
        }

        [Fact]
        public void Unused_currency_should_not_be_allowed()
        {
            Assert.Throws<ArgumentException>(() => Money.FromDecimal(100, "DEM", CurrencyLookup));
        }

        [Fact]
        public void Unknown_currency_should_not_be_allowed()
        {
            Assert.Throws<ArgumentException>(() => Money.FromDecimal(100, "ABC", CurrencyLookup));
        }

        [Fact]
        public void Throw_when_too_many_decimal_places()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Money.FromDecimal(100.111m, "CNY", CurrencyLookup));
        }

        [Fact]
        public void Throws_on_adding_different_currencies()
        {
            Money m1 = Money.FromDecimal(5, "CNY", CurrencyLookup);
            Money m2 = Money.FromDecimal(5, "USD", CurrencyLookup);
            Assert.Throws<CurrencyMismatchException>(() => m1 + m2);
        }

        [Fact]
        public void Throws_on_substracting_different_currencies()
        {
            Money m1 = Money.FromDecimal(5, "CNY", CurrencyLookup);
            Money m2 = Money.FromDecimal(5,"USD",CurrencyLookup);
            Assert.Throws<CurrencyMismatchException>(() => m1 + m2);
        }
    }
}
