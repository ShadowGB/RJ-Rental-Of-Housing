namespace RJRentalOfHousing.Domain
{
    public record Area
    {
        public decimal Value { get; internal set; }

        public Area(decimal value)
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException("面积必须大于0");
            Value = value;
        }
    }
}
