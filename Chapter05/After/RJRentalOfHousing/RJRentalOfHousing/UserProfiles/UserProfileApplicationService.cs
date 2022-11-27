using RJRentalOfHousing.Domain.Shared;
using RJRentalOfHousing.Domain.UserProfiles;
using RJRentalOfHousing.Framework;
using static RJRentalOfHousing.UserProfiles.Contracts;

namespace RJRentalOfHousing.UserProfiles
{
    public class UserProfileApplicationService : IApplicationService
    {
        private readonly IUserProfileRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CheckTextForProfanity _checkTextForProfanity;

        public UserProfileApplicationService(IUserProfileRepository repository,IUnitOfWork unitOfWork,CheckTextForProfanity checkTextForProfanity)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _checkTextForProfanity = checkTextForProfanity;
        }

        private async Task HandleUpdate(Guid userProfileId,Action<UserProfile> operation)
        {
            var apartment = await _repository.Load(new UserId(userProfileId));
            if (apartment == null)
                throw new InvalidOperationException($"{userProfileId}不存在");
            operation(apartment);
            await _unitOfWork.Commit();
        }
        
        public async Task Handle(object command)
        {
            switch(command) 
            {
                case V1.RegisterUser cmd:
                    if (await _repository.Exists(new UserId(cmd.UserId)))
                        throw new InvalidOperationException($"{cmd.UserId}已存在");
                    var userProfile = new UserProfile(new UserId(cmd.UserId), FullName.FromString(cmd.FullName),DisplayName.FromString(cmd.DisplayName,_checkTextForProfanity));
                    await _repository.Add(userProfile);
                    await _unitOfWork.Commit();
                    break;
                case V1.UpdateUserFullName cmd:
                    await HandleUpdate(cmd.UserId, profile =>profile.UpdateFullName(FullName.FromString(cmd.FullName)));
                    break;
                case V1.UpdateDisplayName cmd:
                    await HandleUpdate(cmd.UserId, profile => profile.UpdateDisplayName(DisplayName.FromString(cmd.DisplayName, _checkTextForProfanity)));
                    break;
                case V1.UpdateUserProfilePhoto cmd:
                    await HandleUpdate(cmd.UserId, profile => profile.UpdateProfilePhoto(new Uri(cmd.PhotoUrl)));
                    break;
                default:
                    throw new InvalidOperationException($"未知的命令类型{command.GetType().FullName}");
            }
        }
    }
}
