using Aerothon.Helper.Interfaces;
using Aerothon.Models.Entities;

namespace Aerothon.Helper
{
    /// <summary>
    /// Graph helper
    /// </summary>
    public class GraphHelper : IGraphHelper
    {
        /// <summary>
        /// Adjacency list
        /// </summary>
        private Dictionary<Waypoint, Dictionary<Waypoint, double>> _adjacencyList;

        /// <summary>
        /// Graph helper
        /// </summary>
        public GraphHelper()
        {
            _adjacencyList = new Dictionary<Waypoint, Dictionary<Waypoint, double>>();
        }

        /// <summary>
        /// Add edge
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="destination">destination</param>
        /// <param name="weight">weight</param>
        public void AddEdge(Waypoint source, Waypoint destination, double weight)
        {
            if (!_adjacencyList.ContainsKey(source))
                _adjacencyList[source] = new Dictionary<Waypoint, double>();

            _adjacencyList[source][destination] = weight;
        }

        /// <summary>
        /// Create graph
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="destination">destination</param>
        /// <param name="waypointsBetween">way points between</param>
        public void CreateGraph(
            Waypoint source,
            Waypoint destination,
            List<Waypoint> waypointsBetween
        )
        {
            var allWaypoints = new List<Waypoint>(waypointsBetween) { source, destination };

            // Add edges between all waypoints
            foreach (var wp1 in allWaypoints)
            {
                foreach (var wp2 in allWaypoints)
                {
                    if (wp1 != wp2)
                    {
                        var wayPointHelper = new WaypointHelper(wp1.Latitude, wp1.Longitude);
                        double distance = wayPointHelper.DistanceTo(wp2);
                        AddEdge(wp1, wp2, distance);
                    }
                }
            }
        }

        /// <summary>
        /// Dijkstra implementaion
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="destination">destination</param>
        /// <returns></returns>
        public List<Waypoint> Dijkstra(Waypoint source, Waypoint destination)
        {
            var distances = new Dictionary<Waypoint, double>();
            var previous = new Dictionary<Waypoint, Waypoint>();
            var priorityQueue = new List<Waypoint>();

            foreach (var vertex in _adjacencyList.Keys)
            {
                if (vertex == source)
                    distances[vertex] = 0;
                else
                    distances[vertex] = double.MaxValue;

                priorityQueue.Add(vertex);
            }

            while (priorityQueue.Count != 0)
            {
                priorityQueue.Sort((x, y) => distances[x].CompareTo(distances[y]));
                var u = priorityQueue[0];
                priorityQueue.RemoveAt(0);

                if (u == destination)
                    break;

                if (distances[u] == double.MaxValue)
                    break;

                foreach (var neighbor in _adjacencyList[u])
                {
                    var alt = distances[u] + neighbor.Value;
                    if (alt < distances[neighbor.Key])
                    {
                        distances[neighbor.Key] = alt;
                        previous[neighbor.Key] = u;
                    }
                }
            }

            var path = new List<Waypoint>();
            var current = destination;
            while (previous.ContainsKey(current))
            {
                path.Insert(0, current);
                current = previous[current];
            }

            // Add source to the beginning of the path
            path.Insert(0, source);

            return path;
        }

        /// <summary>
        /// Find k shortest path
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="destination">destination</param>
        /// <param name="k"></param>
        /// <returns>list of paths</returns>
        public List<List<Waypoint>> FindKShortestPaths(Waypoint source, Waypoint destination, int k)
        {
            var kPaths = GenerateAlternateRoutes(source, destination, 3);

            return kPaths;
        }

        /// <summary>
        /// Generates the alternate routes.
        /// </summary>
        /// <param name="startCoordinate">The start coordinate.</param>
        /// <param name="endCoordinate">The end coordinate.</param>
        /// <param name="k">The k.</param>
        /// <returns>alternate paths </returns>
        private List<List<Waypoint>> GenerateAlternateRoutes(
            Waypoint startCoordinate,
            Waypoint endCoordinate,
            int k
        )
        {
            var routes = new List<List<Waypoint>>();

            for (int i = 0; i < k; i++)
            {
                var route = new List<Waypoint>();
                route.Add(startCoordinate);

                double latDiff = (endCoordinate.Latitude - startCoordinate.Latitude) / 6;
                double lonDiff = (endCoordinate.Longitude - startCoordinate.Longitude) / 6;

                for (int j = 1; j <= 5; j++)
                {
                    double nextLat = startCoordinate.Latitude + j * latDiff;
                    double nextLon = startCoordinate.Longitude + j * lonDiff;
                    route.Add(
                        new Waypoint { Latitude = (float)nextLat, Longitude = (float)nextLon }
                    );
                }

                route.Add(endCoordinate);
                routes.Add(route);
            }

            return routes;
        }

        /// <summary>
        /// calcukate distance
        /// </summary>
        /// <param name="path">path</param>
        /// <returns>distance</returns>
        private double CalculatePathDistance(List<Waypoint> path)
        {
            double totalDistance = 0;
            for (int i = 0; i < path.Count - 1; i++)
            {
                totalDistance += _adjacencyList[path[i]][path[i + 1]];
            }
            return totalDistance;
        }
    }
}
