using Aerothon.Models.Entities;
using Aerothon.Models.Response;

namespace Aerothon.Services.Interfaces
{
    public interface IFlightService
    {
        Task<FlightResponse> GetFlightDetailsByIata(string flightIata);
        Task<List<WaypointResponse>> GetAllWaypointsOfFlight(string flightIata);

        /// <summary>
        /// Get alternate paths
        /// </summary>
        /// <param name="currentPosition"></param>
        /// <param name="destination"></param>
        /// <returns>list of alternate paths.</returns>
        List<List<Waypoint>> GetAlternatePaths(Waypoint currentPosition, Waypoint destination);
    }
}
