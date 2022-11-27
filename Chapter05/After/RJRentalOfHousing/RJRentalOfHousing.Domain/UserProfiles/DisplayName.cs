using RJRentalOfHousing.Domain.Shared;

namespace RJRentalOfHousing.Domain.UserProfiles
{
    public record DisplayName
    {
        public string Value { get; internal set; }

        internal DisplayName(string displayName) => Value = displayName;

        public static DisplayName FromString(string displayName,CheckTextForProfanity hasProfanity)
        {
            if (string.IsNullOrEmpty(displayName))
                throw new ArgumentNullException(nameof(displayName));
            if (hasProfanity(displayName))
                throw new DomainExceptions.ProfanityFound(displayName);
            return new DisplayName(displayName);
        }

        public static implicit operator string(DisplayName displayName) => displayName.Value;

        protected DisplayName() { }
    }
}
