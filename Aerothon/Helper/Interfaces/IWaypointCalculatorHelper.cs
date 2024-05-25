using Aerothon.Models.Entities;

namespace Aerothon.Helper
{
    /// <summary>
    /// IWaypointCalculatorHelper
    /// </summary>
    public interface IWaypointCalculatorHelper
    {
        /// <summary>
        /// Calculates the great circle path.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <returns></returns>
        List<Waypoint> CalculateWayPoints(Waypoint source, Waypoint destination);

        /// <summary>
        /// Gets the coordinates by airport.
        /// </summary>
        /// <param name="airport">The airport.</param>
        /// <returns></returns>
        Task<Waypoint> GetCoordinatesByAirport(string airport);
    }
}
