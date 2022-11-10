namespace RJRentalOfHousing.Domain
{
    public record Price : Money
    {
        public Price(decimal amount) : base(amount)
        {
            if (amount < 0)
                throw new ArgumentException("价格不能小于0", nameof(amount));
        }
    }
}
