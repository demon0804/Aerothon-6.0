using Aerothon.Models.Entities;

namespace Aerothon.Helper
{
    /// <summary>
    /// IWaypointHelper
    /// </summary>
    public interface IWaypointHelper
    {
        /// <summary>
        /// Calculates the great circle path.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <returns></returns>
        List<Waypoint> CalculateGreatCirclePath(Waypoint source, Waypoint destination);

        /// <summary>
        /// Gets the coordinates by airport.
        /// </summary>
        /// <param name="airport">The airport.</param>
        /// <returns></returns>
        Task<Waypoint> GetCoordinatesByAirport(string airport);
    }
}
