namespace RJRentalOfHousing.Domain
{
    public record UserId
    {
        public Guid Value { get; internal set; }

        public UserId(Guid value)
        {
            if (value == default)
                throw new ArgumentException("用户编号不能为空");
            Value = value;
        }

        public static implicit operator Guid(UserId value) => value.Value;
    }
}
