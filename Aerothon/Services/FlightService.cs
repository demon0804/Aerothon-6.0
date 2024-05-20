using Aerothon.Models.Response;
using Aerothon.Repository.Interfaces;
using Aerothon.Services.Interfaces;

namespace Aerothon.Services
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _flightrepository;

        public FlightService(IFlightRepository flightrepository)
        {
            _flightrepository = flightrepository;
        }

        public FlightResponse getFlightDetailsById(string flightId)
        {
            var flightDetails = _flightrepository.getFlightDetailsById(flightId);

            var lastPositionR = new WaypointResponse();
            lastPositionR.Lattitude = flightDetails.LastPosition.Lattitude;
            lastPositionR.Longitude = flightDetails.LastPosition.Longitude;
            lastPositionR.Weather = flightDetails.LastPosition.Weather;

            FlightResponse flightresponse =
                new()
                {
                    Id = flightDetails.Id,
                    LastPosition = lastPositionR,
                    Source = flightDetails.Source,
                    Destination = flightDetails.Destination
                };

            return flightresponse;
        }

        public List<WaypointResponse> getAllWaypointsOfFlight(string flightId)
        {
            var waypoints = _flightrepository.getAllWaypointsOfFlight(flightId);

            List<WaypointResponse> waypointResponses = waypoints
                .Select(w => new WaypointResponse
                {
                    Lattitude = w.Lattitude,
                    Longitude = w.Longitude,
                    Weather = w.Weather
                })
                .ToList();

            return waypointResponses;
        }
    }
}
