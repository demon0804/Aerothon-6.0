using Aerothon.Models.Entities;
using Aerothon.Models.Response;

namespace Aerothon.Services.Interfaces
{
    public interface IFlightService
    {
        FlightResponse getFilghtDetailsById(string flightId);
        List<WaypointResponse> getAllWaypointsOfFlight(string flightId);
    }
}
