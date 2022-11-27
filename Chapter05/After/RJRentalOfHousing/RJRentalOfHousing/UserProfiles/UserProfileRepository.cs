using RJRentalOfHousing.Domain.Shared;
using RJRentalOfHousing.Domain.UserProfiles;
using RJRentalOfHousing.Infrastructure;

namespace RJRentalOfHousing.UserProfiles
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly RentalDBContext _dbContext;

        public UserProfileRepository(RentalDBContext dbContext) => _dbContext = _dbContext;


        public async Task Add(UserProfile entity) => await _dbContext.UserProfiles.AddAsync(entity);

        public async Task<bool> Exists(UserId id) => await _dbContext.UserProfiles.FindAsync(id.Value) != null;

        public async Task<UserProfile> Load(UserId id) => await _dbContext.UserProfiles.FindAsync(id.Value);
    }
}
