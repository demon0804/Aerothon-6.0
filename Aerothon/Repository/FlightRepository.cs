using Aerothon.Models.Entities;
using Aerothon.Models.Response;
using Aerothon.Repository.Interfaces;

namespace Aerothon.Repository
{
    public class FlightRepository : IFlightRepository
    {
        public FlightRepository()
        {

        }
       private readonly List<Flight> flightResponses = new List<Flight>
        {
          new Flight
        {
          Id = "12345",
          LastPosition = new Waypoint { Lattitude = 40.7128, Longitude = -74.0060 ,Weather= true},
          Source = "JFK",
          Destination = "LAX"
        },
       new Flight
       {
        Id = "67890",
        LastPosition = new Waypoint { Lattitude = 34.0522, Longitude = -118.2437 ,Weather=true},
        Source = "LAX",
        Destination = "JFK"
       }
     };

 private readonly List<WayPointsTrack> waypointsCollection = new List<WayPointsTrack>
{
    new WayPointsTrack
    {
        FlightId = "12345",
        waypoints = new List<Waypoint>
        {
            new Waypoint { Lattitude = 40.7128, Longitude = -74.0060, Weather = true },
            new Waypoint { Lattitude = 40.7128, Longitude = -74.0060, Weather = true },

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
