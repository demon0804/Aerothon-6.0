using Aerothon.Models.Response;

namespace Aerothon.Services.Interfaces
{
    public interface IFlightService
    {
        Task<FlightResponse> GetFlightDetailsByIata(string flightIata);
        List<WaypointResponse> GetAllWaypointsOfFlight(string flightIata);
    }
}
