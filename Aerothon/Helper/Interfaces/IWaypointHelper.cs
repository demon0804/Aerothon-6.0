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
        /// Gets the coordinates by iata.
        /// </summary>
        /// <param name="iata">The iata.</param>
        /// <returns></returns>
        Waypoint GetCoordinatesByIata(string iata);
    }
}
