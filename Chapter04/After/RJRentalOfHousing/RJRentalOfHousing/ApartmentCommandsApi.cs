using Microsoft.AspNetCore.Mvc;
using static RJRentalOfHousing.Contracts.Apartments;

namespace RJRentalOfHousing
{
    [Route("/apartment")]
    public class ApartmentCommandsApi : Controller
    {
        private readonly ApartmentsApplicationService _applicationService;
        private static Serilog.ILogger Log = Serilog.Log.ForContext<ApartmentCommandsApi>();

        public ApartmentCommandsApi(ApartmentsApplicationService applicationService) => _applicationService = applicationService;

        private async Task<IActionResult> HandleRequest<T>(T request,Func<T,Task> handler)
        {
            try
            {
                Log.Debug("正在处理类型为{}的HTTP请求", typeof(T).Name);
                await handler(request);
                return Ok();
            }
            catch (Exception e)
            {
                Log.Error("处理请求失败", e);
                return new BadRequestObjectResult(new { error = e.Message, stackTrace = e.StackTrace });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]V1.Create request) => await HandleRequest(request, _applicationService.Handle);

        [Route("area")]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] V1.SetArea request) => await HandleRequest(request, _applicationService.Handle);

        [Route("address")]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] V1.SetAddress request) => await HandleRequest(request, _applicationService.Handle);

        [Route("rent")]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] V1.SetRent request) => await HandleRequest(request, _applicationService.Handle);

        [Route("deposit")]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] V1.SetDeposit request) => await HandleRequest(request, _applicationService.Handle);

        [Route("remark")]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] V1.SetRemark request) => await HandleRequest(request, _applicationService.Handle);

        [Route("publish")]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] V1.SentForReview request) => await HandleRequest(request, _applicationService.Handle);
    }
}
