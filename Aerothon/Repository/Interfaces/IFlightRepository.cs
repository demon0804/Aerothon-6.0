using Aerothon.Models.Entities;

namespace Aerothon.Repository.Interfaces
{
    /// <summary>
    /// IFlightRepository
    /// </summary>
    public interface IFlightRepository
    {
        /// <summary>
        /// Gets the flight details by iata.
        /// </summary>
        /// <param name="flightIata">The flight iata.</param>
        /// <returns></returns>
        Task<Flight> GetFlightDetailsByIata(string flightIata);

        /// <summary>
        /// Gets all waypoints of flight.
        /// </summary>
        /// <param name="flightIata">The flight iata.</param>
        /// <returns></returns>
        Task<List<Waypoint>> GetAllWaypointsOfFlight(string flightIata);
    }
}
