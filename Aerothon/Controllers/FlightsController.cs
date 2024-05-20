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
        public IActionResult GetFlightDetailsById([FromRoute] string id)
        {
            var flightDetails = _flightservice.getFlightDetailsById(id);
            return Ok(flightDetails);
        }

        [HttpGet("flights/{id}/track")]
        public IActionResult GetAllWaypointsOfFlight(string id)
        {
            var wayPoints = _flightservice.getAllWaypointsOfFlight(id);
            return Ok(wayPoints);
        }
    }
}
