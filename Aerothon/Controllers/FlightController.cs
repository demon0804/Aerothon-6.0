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
        [HttpGet("flights/{id}")]
        public IActionResult getFilghtDetailsById([FromRoute]string id)
        {
           var flightDetails= _flightservice.getFlightDetailsById(id);
            return Ok(flightDetails);
        }

        [HttpGet("flights/{id}/track")]
        public IActionResult getAllWaypointsOfFlight(string id)
        {
            var wayPoints = _flightservice.getAllWaypointsOfFlight(id);
            return Ok(wayPoints);
        }
    }
}
