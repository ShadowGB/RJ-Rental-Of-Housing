using Microsoft.AspNetCore.Mvc;
using RJRentalOfHousing.Infrastructure;
using Serilog;
using System.Data.Common;

namespace RJRentalOfHousing.Apartments
{
    [Route("/apartment")]
    [ApiController]
    public class ApartmentsQueryApi : Controller
    {
        private readonly DbConnection _dbConnection;
        private static Serilog.ILogger _log = Log.ForContext<ApartmentsQueryApi>();

        public ApartmentsQueryApi(DbConnection dbConnection) => _dbConnection = dbConnection;


        [HttpGet]
        [Route("createlist")]
        public Task<IActionResult> Query([FromQuery]QueryModels.GetCreatedApartment request)
            => RequestHandler.HandleQuery(() => _dbConnection.Query(request), _log);

        [HttpGet]
        [Route("getall")]
        public Task<IActionResult> Query([FromQuery] QueryModels.GetAllApartmentsByPage request)
            => RequestHandler.HandleQuery(() => _dbConnection.Query(request), _log);

        [HttpGet]
        [Route("getowners")]
        public Task<IActionResult> Query([FromQuery] QueryModels.GetOwnersApartment request)
            => RequestHandler.HandleQuery(() => _dbConnection.Query(request), _log);

        [HttpGet]
        [Route("getdetail")]
        public Task<IActionResult> Query([FromQuery] QueryModels.GetApartmentDetailById request)
            => RequestHandler.HandleQuery(() => _dbConnection.Query(request), _log);
    }
}
