using RJRentalOfHousing.Domain.Shared;

namespace RJRentalOfHousing.Domain.UserProfiles
{
    public interface IUserProfileRepository
    {
        Task<UserProfile> Load(UserId id);
        Task Add(UserProfile entity);
        Task<bool> Exists(UserId id);
    }
}
