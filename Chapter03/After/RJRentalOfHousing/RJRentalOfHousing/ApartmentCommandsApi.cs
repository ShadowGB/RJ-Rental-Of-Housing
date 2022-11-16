using Microsoft.AspNetCore.Mvc;
using static RJRentalOfHousing.Contracts.Apartments;

namespace RJRentalOfHousing
{
    [Route("/apartment")]
    public class ApartmentCommandsApi : Controller
    {
        private readonly ApartmentsApplicationService _applicationService;

        public ApartmentCommandsApi(ApartmentsApplicationService applicationService) => _applicationService = applicationService;

        [HttpPost]
        public async Task<IActionResult> Post(V1.Create request)
        {
            await _applicationService.Handle(request);
            return Ok();
        }

        [Route("area")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.SetArea request)
        {
            await _applicationService.Handle(request);
            return Ok();
        }

        [Route("address")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.SetAddress request)
        {
            await _applicationService.Handle(request);
            return Ok();
        }

        [Route("rent")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.SetRent request)
        {
            await _applicationService.Handle(request);
            return Ok();
        }

        [Route("deposit")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.SetDeposit request)
        {
            await _applicationService.Handle(request);
            return Ok();
        }

        [Route("remark")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.SetRemark request)
        {
            await _applicationService.Handle(request);
            return Ok();
        }

        [Route("publish")]
        [HttpPut]
        public async Task<IActionResult> Put(V1.SentForReview request)
        {
            await _applicationService.Handle(request);
            return Ok();
        }
    }
}
