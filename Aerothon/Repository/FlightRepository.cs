using Aerothon.Models.Entities;
using Aerothon.Repository.Interfaces;

namespace Aerothon.Repository
{
    public class FlightRepository : IFlightRepository
    {
        public FlightRepository() { }

        private readonly List<Flight> flightResponses =
            new()
            {
                new()
                {
                    Id = "12345",
                    LastPosition = new Waypoint
                    {
                        Lattitude = 40.7128f,
                        Longitude = -74.0060f,
                        Weather = "Yes"
                    },
                    Source = "JFK",
                    Destination = "LAX"
                },
                new()
                {
                    Id = "67890",
                    LastPosition = new Waypoint
                    {
                        Lattitude = 34.0522f,
                        Longitude = -118.2437f,
                        Weather = "Yes"
                    },
                    Source = "LAX",
                    Destination = "JFK"
                }
            };

        private readonly List<WayPointsTrack> waypointsCollection =
            new()
            {
                new WayPointsTrack
                {
                    FlightId = "12345",
                    waypoints = new List<Waypoint>
                    {
                        new()
                        {
                            Lattitude = 40.7128f,
                            Longitude = -74.0060f,
                            Weather = "Yes"
                        },
                        new()
                        {
                            Lattitude = 40.7128f,
                            Longitude = -74.0060f,
                            Weather = "Yes"
                        },
                    }
                }
            };

        public Flight getFlightDetailsById(string flightId)
        {
            return flightResponses.FirstOrDefault(f => f.Id == flightId);
        }

        public List<Waypoint> getAllWaypointsOfFlight(string flightId)
        {
            var waypointsTrack = waypointsCollection.FirstOrDefault(f => f.FlightId == flightId);
            return waypointsTrack.waypoints.ToList();
        }
    }
}
