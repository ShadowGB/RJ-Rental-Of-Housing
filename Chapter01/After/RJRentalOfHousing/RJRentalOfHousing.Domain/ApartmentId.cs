namespace RJRentalOfHousing.Domain
{
    public record ApartmentId
    {
        public Guid Value { get; internal set; }

        public ApartmentId(Guid value)
        {
            if (value == default)
                throw new ArgumentException("Id不能为空");
            Value = value;
        }
    }
}
