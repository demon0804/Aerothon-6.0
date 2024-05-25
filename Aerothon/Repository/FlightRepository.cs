using Aerothon.Helper;
using Aerothon.Helper.Interfaces;
using Aerothon.Models.Entities;
using Aerothon.Repository.Interfaces;
using Newtonsoft.Json;

namespace Aerothon.Repository
{
    /// <summary>
    /// FlightRepository
    /// </summary>
    /// <seealso cref="Aerothon.Repository.Interfaces.IFlightRepository" />
    public class FlightRepository : IFlightRepository
    {
        /// <summary>
        /// The weather helper
        /// </summary>
        private readonly IWeatherHelper _weatherHelper;
        private readonly IWaypointCalculatorHelper _WaypointCalculatorHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlightRepository"/> class.
        /// </summary>
        /// <param name="weatherHelper">The weather helper.</param>
        public FlightRepository(
            IWeatherHelper weatherHelper,
            IWaypointCalculatorHelper WaypointCalculatorHelper
        )
        {
            _weatherHelper = weatherHelper;
            _WaypointCalculatorHelper = WaypointCalculatorHelper;
        }

        /// <summary>
        /// Gets the flight details by iata.
        /// </summary>
        /// <param name="flightIata">The flight iata.</param>
        /// <returns></returns>
        public async Task<Flight> GetFlightDetailsByIata(string flightIata)
        {
            string apiKey = "919c87e419c9f9a5e06b16e2443e5b25";
            string apiUrl =
                $"http://api.aviationstack.com/v1/flights?access_key={apiKey}&flight_iata={flightIata}&flight_status=active";

            HttpClient client = new();
            HttpResponseMessage response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var flightDetails = new Flight();

                string responseData = await response.Content.ReadAsStringAsync();
                dynamic responseJson = JsonConvert.DeserializeObject(responseData);

                if (responseJson.pagination.count < 1)
                {
                    return null;
                }

                flightDetails.Id = responseJson.data[0].flight.iata;
                if (responseJson.data[0].live != null)
                {
                    float Latitude = responseJson.data[0].live.latitude;
                    float Longitude = responseJson.data[0].live.longitude;
                    flightDetails.LastPosition = new Waypoint
                    {
                        Latitude = Latitude,
                        Longitude = Longitude,
                        IsSafeToTravel = await _weatherHelper.CheckWeatherIsSafeToTravel(
                            Latitude,
                            Longitude
                        )
                    };
                }
                else
                {
                    string airport = responseJson.data[0].departure.airport;
                    Waypoint coordinates = await _WaypointCalculatorHelper.GetCoordinatesByAirport(
                        airport
                    );
                    flightDetails.LastPosition = new Waypoint
                    {
                        Latitude = coordinates.Latitude,
                        Longitude = coordinates.Longitude,
                        IsSafeToTravel = await _weatherHelper.CheckWeatherIsSafeToTravel(
                            coordinates.Latitude,
                            coordinates.Longitude
                        )
                    };
                }
                flightDetails.Source = new()
                {
                    Airport = responseJson.data[0].departure.airport,
                    Timezone = responseJson.data[0].departure.timezone,
                    IATA = responseJson.data[0].departure.iata,
                    ICAO = responseJson.data[0].departure.icao,
                    Scheduled = responseJson.data[0].departure.scheduled
                };
                flightDetails.Destination = new()
                {
                    Airport = responseJson.data[0].arrival.airport,
                    Timezone = responseJson.data[0].arrival.timezone,
                    IATA = responseJson.data[0].arrival.iata,
                    ICAO = responseJson.data[0].arrival.icao,
                    Scheduled = responseJson.data[0].arrival.scheduled
                };
                return flightDetails;
            }
            return null;
        }

        /// <summary>
        /// Gets all waypoints of flight.
        /// </summary>
        /// <param name="flightIata">The flight iata.</param>
        /// <returns></returns>
        public async Task<List<Waypoint>> GetAllWaypointsOfFlight(string flightIata)
        {
            //Fetch flight details based on flightIata
            Flight flightDetails = await GetFlightDetailsByIata(flightIata);
            if (flightDetails == null)
            {
                return new List<Waypoint>(); // Return empty list if no flight details found
            }

            //Extract source and destination waypoints from flight details
            var source = await _WaypointCalculatorHelper.GetCoordinatesByAirport(
                flightDetails.Source.Airport
            );
            var destination = await _WaypointCalculatorHelper.GetCoordinatesByAirport(
                flightDetails.Destination.Airport
            );

            // Calculate waypoints along the great circle path
            var waypoints = _WaypointCalculatorHelper.CalculateWayPoints(source, destination);

            // Optionally, enrich waypoints with weather data
            foreach (var waypoint in waypoints)
            {
                waypoint.IsSafeToTravel = await _weatherHelper.CheckWeatherIsSafeToTravel(
                    waypoint.Latitude,
                    waypoint.Longitude
                );
            }

            return waypoints;
        }
    }
}
