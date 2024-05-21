using Aerothon.Models.Entities;

namespace Aerothon.Repository.Interfaces
{
    public interface IFlightRepository
    {
        Task<Flight> GetFlightDetailsByIata(string flightIata);
        List<Waypoint> GetAllWaypointsOfFlight(string flightIata);
    }
}
