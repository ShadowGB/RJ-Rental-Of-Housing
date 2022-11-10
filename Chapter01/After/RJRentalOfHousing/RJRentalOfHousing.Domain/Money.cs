namespace RJRentalOfHousing.Domain
{
    public record Money
    {
        public decimal Amount { get; internal set; }

        public Money(decimal amount)
        {
            Amount = amount;
        }
    }
}
