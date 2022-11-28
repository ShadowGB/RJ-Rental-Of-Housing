namespace RJRentalOfHousing.Domain
{
    public record Price : Money
    {
        protected Price(){ }

        private Price(decimal amount, string currencyCode, ICurrencyLookup currencyLookup) : base(amount, currencyCode, currencyLookup)
        {
            if (amount < 0)
                throw new ArgumentException("价格不能为负数", nameof(amount));
        }

        internal Price(decimal amount,string currencyCode):base(amount,new CurrencyDetails { CurrencyCode=currencyCode })
        {

        }


        public static Price FromDecimal(decimal amount, string currency, ICurrencyLookup currencyLookup)
            => new Price(amount, currency, currencyLookup);

        public static Price NoPrice = new Price();
    }
}
