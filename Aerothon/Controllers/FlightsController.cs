using Aerothon.Models.Entities;
using Aerothon.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Aerothon.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightService _flightservice;

        public FlightsController(IFlightService flightservice)
        {
            _flightservice = flightservice;
        }

        [HttpGet("{id}")]
        public IActionResult GetFlightDetailsById([FromRoute] string id)
        {
            var flightDetails = _flightservice.getFlightDetailsById(id);
            return Ok(flightDetails);
        }

        [HttpGet("{id}/track")]
        public IActionResult GetAllWaypointsOfFlight(string id)
        {
            var wayPoints = _flightservice.getAllWaypointsOfFlight(id);
            return Ok(wayPoints);
        }

        /// <summary>
        /// GetAlternatePaths
        /// </summary>
        /// <param name="wayPoints">way points</param>
        /// <returns>Alternate paths</returns>
        [HttpGet("paths")]
        public IActionResult GetAlternatePaths([FromBody]List<Waypoint> wayPoints)
        {
            var result = _flightservice.GetAlternatePaths(wayPoints[0], wayPoints[1]);
            return Ok(result);
        }
    }
}
