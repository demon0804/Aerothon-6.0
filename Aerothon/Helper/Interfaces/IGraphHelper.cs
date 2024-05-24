using Aerothon.Models.Entities;

namespace Aerothon.Helper.Interfaces
{
    public interface IGraphHelper
    {
        /// <summary>
        /// Add edge
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="destination">destination</param>
        /// <param name="weight">weight</param>
        void AddEdge(Waypoint source, Waypoint destination, double weight);

        /// <summary>
        /// Create graph
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="destination">destination</param>
        /// <param name="waypointsBetween">way points between</param>
        void CreateGraph(Waypoint source, Waypoint destination, List<Waypoint> waypointsBetween);

        /// <summary>
        /// Dijkstra implementaion
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="destination">destination</param>
        /// <returns></returns>
        List<Waypoint> Dijkstra(Waypoint source, Waypoint destination);

        /// <summary>
        /// Find k shortest path
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="destination">destination</param>
        /// <param name="k"></param>
        /// <returns>list of paths</returns>
        List<List<Waypoint>> FindKShortestPaths(Waypoint source, Waypoint destination, int k);
    }
}
