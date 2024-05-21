﻿using Aerothon.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Aerothon.Controllers
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("/[controller]")]
    public class FlightsController : ControllerBase
    {
        /// <summary>
        /// The flightservice
        /// </summary>
        private readonly IFlightService _flightservice;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlightsController"/> class.
        /// </summary>
        /// <param name="flightservice">The flightservice.</param>
        public FlightsController(IFlightService flightservice)
        {
            _flightservice = flightservice;
        }

        /// <summary>
        /// Gets the flight details by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFlightDetailsById([FromRoute] string id)
        {
            var flightDetails = await _flightservice.GetFlightDetailsByIata(id);
            return Ok(flightDetails);
        }

        /// <summary>
        /// Gets all waypoints of flight.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}/track")]
        public async Task<IActionResult> GetAllWaypointsOfFlight(string id)
        {
            var wayPoints = await _flightservice.GetAllWaypointsOfFlight(id);
            return Ok(wayPoints);
        }
    }
}
