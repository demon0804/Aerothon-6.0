using Aerothon.Models.Entities;
using Aerothon.Models.Response;
using Aerothon.Repository.Interfaces;

namespace Aerothon.Repository
{
    public class FlightRepository : IFlightRepository
    {
       private readonly List<Flight> flightResponses = new List<Flight>
        {
          new Flight
        {
          FlightID = "12345",
          LastPosition = new Waypoint { lattitude = 40.7128, longitude = -74.0060 ,Weather= true},
          Source = "JFK",
          Destination = "LAX"
        },
       new Flight
       {
        FlightID = "67890",
        LastPosition = new Waypoint { lattitude = 34.0522, longitude = -118.2437 ,Weather=true},
        Source = "LAX",
        Destination = "JFK"
       }
     };

 private readonly List<WayPointsTrack> waypointsCollection = new List<WayPointsTrack>
{
    new WayPointsTrack
    {
        flightId = "12345",
        waypoints = new List<Waypoint>
        {
            new Waypoint { lattitude = 40.7128, longitude = -74.0060, Weather = true },
            new Waypoint { lattitude = 40.7128, longitude = -74.0060, Weather = true },

        }
    }
};


        public Flight getFilghtDetailsById(string flightId)
        {
            return flightResponses.FirstOrDefault(f => f.FlightID == flightId);
        }

        public List<Waypoint> getAllWaypointsOfFlight(string flightId)
        {
            var waypointsTrack = waypointsCollection.FirstOrDefault(f => f.flightId == flightId);
            return waypointsTrack.waypoints.ToList();
        }

    }
}
