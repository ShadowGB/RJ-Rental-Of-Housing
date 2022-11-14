namespace RJRentalOfHousing.Domain
{
    public record Address
    {
        public string Value { get; internal set; }

        public Address(string value)
        {
            if(string.IsNullOrEmpty(value))
                throw new ArgumentException("地址不能为空");
            Value = value;
        }
    }
}
