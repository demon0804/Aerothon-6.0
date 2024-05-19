using Aerothon.Models.Entities;
using Aerothon.Models.Response;
using Aerothon.Repository;
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

       

        public FlightResponse getFilghtDetailsById(string flightId)
        {
            var flightDetails= _flightrepository.getFilghtDetailsById(flightId);

            var lastPositionR = new WaypointResponse();
            lastPositionR.lattitude = flightDetails.LastPosition.lattitude;
            lastPositionR.longitude = flightDetails.LastPosition.longitude;
            lastPositionR.Weather = flightDetails.LastPosition.Weather;


            FlightResponse flightresponse = new FlightResponse
            {
               FlightID=flightDetails.FlightID,
               LastPosition=lastPositionR,              
               Source=flightDetails.Source,
               Destination=flightDetails.Destination
            };

            return flightresponse;
        }


        public List<WaypointResponse> getAllWaypointsOfFlight(string flightId)
        {
            var waypoints = _flightrepository.getAllWaypointsOfFlight(flightId);

            List<WaypointResponse> waypointResponses = waypoints.Select(w => new WaypointResponse
            {
                lattitude = w.lattitude,
                longitude = w.longitude,
                Weather = w.Weather
            }).ToList();

            return waypointResponses;
        }


    }
}
