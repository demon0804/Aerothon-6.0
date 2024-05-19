using Aerothon.Models.Entities;

namespace Aerothon.Repository.Interfaces
{
    public interface IFlightRepository
    {
        Flight getFilghtDetailsById(string flightId);
        List<Waypoint> getAllWaypointsOfFlight(string flightId);
    }
}
