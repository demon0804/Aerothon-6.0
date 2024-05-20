using Aerothon.Models.Entities;
using Aerothon.Models.Response;

namespace Aerothon.Services.Interfaces
{
    public interface IFlightService
    {
        FlightResponse getFlightDetailsById(string flightId);
        List<WaypointResponse> getAllWaypointsOfFlight(string flightId);
    }
}
