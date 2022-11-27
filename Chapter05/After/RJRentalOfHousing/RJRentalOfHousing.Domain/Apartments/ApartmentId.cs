namespace RJRentalOfHousing.Domain.Apartments
{
    public record ApartmentId
    {
        protected ApartmentId() { }
        public Guid Value { get; internal set; }

        public ApartmentId(Guid value)
        {
            if (value == default)
                throw new ArgumentException("Id不能为空");
            Value = value;
        }

        public static implicit operator Guid(ApartmentId value) => value.Value;
    }
}
