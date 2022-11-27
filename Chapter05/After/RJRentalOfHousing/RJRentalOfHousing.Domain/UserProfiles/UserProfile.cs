using RJRentalOfHousing.Domain.Shared;
using RJRentalOfHousing.Framework;

namespace RJRentalOfHousing.Domain.UserProfiles
{
    public class UserProfile : AggregateRoot<UserId>
    {
        protected UserProfile() { }
        public Guid UserProfileId { get; internal set; }
        public FullName? FullName { get; internal set; }
        public DisplayName? DisplayName { get; internal set; }
        public string PhotoUrl { get; internal set; }

        public UserProfile(UserId id, FullName fullName, DisplayName displayName)
            => Apply(new Events.UserRegistered
            {
                UserId = id,
                FullName = fullName,
                DisplayName = displayName
            });

        public void UpdateFullName(FullName fullName)
            => Apply(new Events.UserFullNameUpdated
            {
                UserId = Id,
                FullName = fullName
            });

        public void UpdateDisplayName(DisplayName displayName)
            => Apply(new Events.UserDisplayNameUpdated
            {
                UserId = Id,
                DisplayName = displayName
            });

        public void UpdateProfilePhoto(Uri photoUrl)
            => Apply(new Events.ProfilePhotoUploaded
            {
                UserId = Id,
                PhotoUrl = photoUrl.ToString()
            });

        protected override void EnsureValidState() { }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.UserRegistered e:
                    Id = new UserId(e.UserId);
                    FullName = new FullName(e.FullName);
                    DisplayName = new DisplayName(e.DisplayName);
                    UserProfileId = e.UserId;
                    break;
                case Events.UserFullNameUpdated e:
                    FullName = new FullName(e.FullName);
                    break;
                case Events.UserDisplayNameUpdated e:
                    DisplayName = new DisplayName(e.DisplayName);
                    break;
                case Events.ProfilePhotoUploaded e:
                    PhotoUrl = e.PhotoUrl;
                    break;
            }
        }
    }
}
