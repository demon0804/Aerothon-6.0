using Aerothon.Models.Entities;
using Aerothon.Models.Response;

namespace Aerothon.Services.Interfaces
{
    public interface IFlightService
    {
        /// <summary>
        /// Get flight details
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns>flight response</returns>
        FlightResponse getFlightDetailsById(string flightId);

        /// <summary>
        /// Get all way points of flight
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns>list of waypoints</returns>
        List<WaypointResponse> getAllWaypointsOfFlight(string flightId);

        /// <summary>
        /// Get alternate paths
        /// </summary>
        /// <param name="currentPosition"></param>
        /// <param name="destination"></param>
        /// <returns>list of alternate paths.</returns>
        List<List<Waypoint>> GetAlternatePaths(Waypoint currentPosition, Waypoint destination);
    }
}
