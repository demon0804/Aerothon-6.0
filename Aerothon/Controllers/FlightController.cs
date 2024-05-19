using Aerothon.Models.Response;
using Aerothon.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Aerothon.Controllers
{
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightservice;
        public FlightController(IFlightService flightservice)
        {
            _flightservice = flightservice;
        }
        [HttpGet("flights/{flightId}")]
        public IActionResult getFilghtDetailsById([FromRoute]string flightId)
        {
           var flightDetails= _flightservice.getFilghtDetailsById(flightId);
            return Ok(flightDetails);
        }

        [HttpGet("flights/{flightId}/track")]
        public IActionResult getAllWaypointsOfFlight(string flightId)
        {
            var wayPoints = _flightservice.getAllWaypointsOfFlight(flightId);
            return Ok(wayPoints);
        }
    }
}
