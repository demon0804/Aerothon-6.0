using Aerothon.Helper.Interfaces;
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
        public FlightService(
            IFlightRepository flightrepository,
            IGraphHelper graphHelper)
        {
            _flightrepository = flightrepository;
            _graphHelper = graphHelper;
        }

        /// <summary>
        /// Get flight details by id.
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns>flight response</returns>
        public FlightResponse getFlightDetailsById(string flightId)
        {
            var flightDetails = _flightrepository.getFlightDetailsById(flightId);

            if (flightDetails == null)
            {
                return new FlightResponse();
            }

            var lastPositionR = new WaypointResponse();
            lastPositionR.Lattitude = flightDetails.LastPosition.Lattitude;
            lastPositionR.Longitude = flightDetails.LastPosition.Longitude;
            lastPositionR.Weather = flightDetails.LastPosition.Weather;

            FlightResponse flightresponse =
                new()
                {
                    Id = flightDetails.Id,
                    LastPosition = lastPositionR,
                    Source = flightDetails.Source,
                    Destination = flightDetails.Destination
                };

            return flightresponse;
        }

        /// <summary>
        /// Get all way points of flight
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns>list of way points</returns>
        public List<WaypointResponse> getAllWaypointsOfFlight(string flightId)
        {
            var waypoints = _flightrepository.getAllWaypointsOfFlight(flightId);

            if (waypoints == null)
            {
                return new List<WaypointResponse>();
            }

            List<WaypointResponse> waypointResponses = waypoints
                .Select(w => new WaypointResponse
                {
                    Lattitude = w.Lattitude,
                    Longitude = w.Longitude,
                    Weather = w.Weather
                })
                .ToList();

            return waypointResponses;
        }

        /// <summary>
        /// Get alternate paths
        /// </summary>00
        /// <param name="currentPosition"></param>
        /// <param name="destination"></param>
        /// <returns>list of alternate paths.</returns>
        public List<List<Waypoint>> GetAlternatePaths(Waypoint currentPosition, Waypoint destination)
        {
            var alternatePaths = _graphHelper.FindKShortestPaths(currentPosition, destination, 3);
            return alternatePaths;
        }
    }
}
