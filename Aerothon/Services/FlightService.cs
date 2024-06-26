﻿using Aerothon.Helper.Interfaces;
using Aerothon.Models.Entities;
using Aerothon.Models.Response;
using Aerothon.Repository.Interfaces;
using Aerothon.Services.Interfaces;

namespace Aerothon.Services
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _flightrepository;

        /// <summary>
        /// The graph helper
        /// </summary>
        private readonly IGraphHelper _graphHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlightService"/> class.
        /// </summary>
        /// <param name="flightrepository">The flightrepository.</param>
        /// <param name="graphHelper">The graph helper.</param>
        public FlightService(IFlightRepository flightrepository, IGraphHelper graphHelper)
        {
            _flightrepository = flightrepository;
            _graphHelper = graphHelper;
        }

        /// <summary>
        /// Get flight details by id.
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns>flight response</returns>
        public async Task<FlightResponse> GetFlightDetailsByIata(string flightIata)
        {
            var flightDetails = await _flightrepository.GetFlightDetailsByIata(flightIata);

            if (flightDetails == null)
            {
                return new FlightResponse();
            }
            var lastPositionR = new WaypointResponse();

            if (flightDetails.LastPosition != null)
            {
                lastPositionR.Lattitude = flightDetails.LastPosition.Latitude;
                lastPositionR.Longitude = flightDetails.LastPosition.Longitude;
                lastPositionR.IsSafeToTravel = flightDetails.LastPosition.IsSafeToTravel;
            }

            FlightResponse flightresponse =
                new()
                {
                    Id = flightDetails.Id,
                    LastPosition = lastPositionR,
                    Source = new()
                    {
                        Airport = flightDetails.Source.Airport,
                        Timezone = flightDetails.Source.Timezone,
                        IATA = flightDetails.Source.IATA,
                        ICAO = flightDetails.Source.ICAO,
                        Scheduled = flightDetails.Source.Scheduled,
                    },
                    Destination = new()
                    {
                        Airport = flightDetails.Destination.Airport,
                        Timezone = flightDetails.Destination.Timezone,
                        IATA = flightDetails.Destination.IATA,
                        ICAO = flightDetails.Destination.ICAO,
                        Scheduled = flightDetails.Destination.Scheduled,
                    },
                };

            return flightresponse;
        }

        /// <summary>
        /// Get all way points of flight
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns>list of way points</returns>
        public async Task<List<WaypointResponse>> GetAllWaypointsOfFlight(string flightIata)
        {
            var waypoints = await _flightrepository.GetAllWaypointsOfFlight(flightIata);

            if (waypoints == null)
            {
                return new List<WaypointResponse>();
            }

            List<WaypointResponse> waypointResponses = waypoints
                .Select(w => new WaypointResponse
                {
                    Lattitude = w.Latitude,
                    Longitude = w.Longitude,
                    IsSafeToTravel = w.IsSafeToTravel
                })
                .ToList();

            return waypointResponses;
        }

        /// <summary>
        /// Get alternate paths
        /// </summary>
        /// <param name="currentPosition"></param>
        /// <param name="destination"></param>
        /// <returns>list of alternate paths.</returns>
        public List<List<Waypoint>> GetAlternatePaths(
            Waypoint currentPosition,
            Waypoint destination
        )
        {
            var alternatePaths = _graphHelper.FindKShortestPaths(currentPosition, destination, 3);
            return alternatePaths;
        }
    }
}
