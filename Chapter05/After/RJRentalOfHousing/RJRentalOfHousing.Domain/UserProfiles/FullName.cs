namespace RJRentalOfHousing.Domain.UserProfiles
{
    public record FullName
    {
        public string Value { get; internal set; }

        internal FullName(string fullname) => Value = fullname;

        public static FullName FromString(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
                throw new ArgumentNullException(nameof(fullName));
            return new FullName(fullName);
        }

        public static implicit operator string(FullName fullName) => fullName.Value;

        protected FullName() { }
    }
}
