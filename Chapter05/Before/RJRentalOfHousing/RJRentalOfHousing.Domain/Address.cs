namespace RJRentalOfHousing.Domain
{
    public record Address
    {
        protected Address() { }
        public string Value { get; internal set; }

        public Address(string value)
        {
            if(string.IsNullOrEmpty(value))
                throw new ArgumentException("地址不能为空");
            Value = value;
        }

        public static implicit operator string(Address value) => value.Value;

        public static Address NoAddress = new Address();
    }
}
