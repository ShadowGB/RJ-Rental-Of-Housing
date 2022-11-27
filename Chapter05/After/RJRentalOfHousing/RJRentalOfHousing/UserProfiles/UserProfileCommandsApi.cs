using Microsoft.AspNetCore.Mvc;
using RJRentalOfHousing.Infrastructure;
using static RJRentalOfHousing.UserProfiles.Contracts;

namespace RJRentalOfHousing.UserProfiles
{
    [Route("/profile")]
    public class UserProfileCommandsApi : Controller
    {
        private readonly UserProfileApplicationService _applicationService;
        private static readonly Serilog.ILogger Log = Serilog.Log.ForContext<UserProfileCommandsApi>();

        public UserProfileCommandsApi(UserProfileApplicationService applicationService) => _applicationService = applicationService;

        [HttpPost]
        public Task<IActionResult> Post(V1.RegisterUser request) => RequestHandler.HandleRequest(request, _applicationService.Handle, Log);

        [Route("fullname")]
        [HttpPut]
        public Task<IActionResult> Put(V1.UpdateUserFullName request) => RequestHandler.HandleRequest(request, _applicationService.Handle, Log);

        [Route("displayname")]
        [HttpPut]
        public Task<IActionResult> Put(V1.UpdateDisplayName request) => RequestHandler.HandleRequest(request, _applicationService.Handle, Log);

        [Route("photo")]
        [HttpPut]
        public Task<IActionResult> Put(V1.UpdateUserProfilePhoto request) => RequestHandler.HandleRequest(request, _applicationService.Handle, Log);
    }
}
