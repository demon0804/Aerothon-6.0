using Aerothon.Models.Entities;

namespace Aerothon.Repository.Interfaces
{
    public interface IFlightRepository
    {
        Task<Flight> GetFlightDetailsByIata(string flightIata);
        Task<List<Waypoint>> GetAllWaypointsOfFlight(string flightIata);
    }
}
