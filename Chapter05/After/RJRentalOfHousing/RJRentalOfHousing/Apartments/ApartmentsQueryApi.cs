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
    }
}
