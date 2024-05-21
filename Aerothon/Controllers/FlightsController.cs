using Aerothon.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Aerothon.Controllers
{
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightService _flightservice;

        public FlightsController(IFlightService flightservice)
        {
            _flightservice = flightservice;
        }

        [HttpGet("flights/{id}")]
        public async Task<IActionResult> GetFlightDetailsById([FromRoute] string id)
        {
            var flightDetails = await _flightservice.GetFlightDetailsByIata(id);
            return Ok(flightDetails);
        }

        [HttpGet("flights/{id}/track")]
        public IActionResult GetAllWaypointsOfFlight(string id)
        {
            var wayPoints = _flightservice.GetAllWaypointsOfFlight(id);
            return Ok(wayPoints);
        }
    }
}
