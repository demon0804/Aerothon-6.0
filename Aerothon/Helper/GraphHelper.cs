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
        public void CreateGraph(Waypoint source, Waypoint destination, List<Waypoint> waypointsBetween)
        {
            var allWaypoints = new List<Waypoint>(waypointsBetween) { source, destination };

            // Add edges between all waypoints
            foreach (var wp1 in allWaypoints)
            {
                foreach (var wp2 in allWaypoints)
                {
                    if (wp1 != wp2)
                    {
                        var wayPointHelper = new WaypointHelper(wp1.Lattitude, wp1.Longitude);
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
            var kShortestPaths = new List<List<Waypoint>>();
            var candidates = new List<List<Waypoint>>();

            // Find the shortest path
            var shortestPath = Dijkstra(source, destination);
            kShortestPaths.Add(shortestPath);

            for (int i = 1; i < k; i++)
            {
                var lastPath = kShortestPaths.Last();
                for (int j = 0; j < lastPath.Count - 1; j++)
                {
                    var spurNode = lastPath[j];
                    var rootPath = lastPath.Take(j + 1).ToList();

                    var removedEdges = new List<Tuple<Waypoint, Waypoint, double>>();

                    // Remove the edges that were part of the previous paths
                    foreach (var path in kShortestPaths)
                    {
                        if (path.Count > j && rootPath.SequenceEqual(path.Take(j + 1)))
                        {
                            var edge = Tuple.Create(path[j], path[j + 1], _adjacencyList[path[j]][path[j + 1]]);
                            removedEdges.Add(edge);
                            _adjacencyList[path[j]].Remove(path[j + 1]);
                        }
                    }

                    // Remove the root path nodes from the graph
                    foreach (var rootNode in rootPath)
                    {
                        if (_adjacencyList.ContainsKey(rootNode))
                        {
                            foreach (var neighbor in _adjacencyList[rootNode].Keys.ToList())
                            {
                                removedEdges.Add(Tuple.Create(rootNode, neighbor, _adjacencyList[rootNode][neighbor]));
                                _adjacencyList[rootNode].Remove(neighbor);
                            }
                        }
                    }

                    var spurPath = Dijkstra(spurNode, destination);

                    if (spurPath.Count > 0)
                    {
                        var totalPath = new List<Waypoint>(rootPath);
                        totalPath.AddRange(spurPath.Skip(1));
                        candidates.Add(totalPath);
                    }

                    // Add the removed edges back to the graph
                    foreach (var edge in removedEdges)
                    {
                        if (!_adjacencyList.ContainsKey(edge.Item1))
                            _adjacencyList[edge.Item1] = new Dictionary<Waypoint, double>();

                        _adjacencyList[edge.Item1][edge.Item2] = edge.Item3;
                    }
                }

                if (candidates.Count == 0)
                    break;

                candidates = candidates.OrderBy(path => CalculatePathDistance(path)).ToList();

                kShortestPaths.Add(candidates.First());
                candidates.RemoveAt(0);
            }

            return kShortestPaths;
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
