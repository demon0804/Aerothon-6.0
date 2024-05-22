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
        private readonly IWaypointHelper _waypointHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlightRepository"/> class.
        /// </summary>
        /// <param name="weatherHelper">The weather helper.</param>
        public FlightRepository(IWeatherHelper weatherHelper, IWaypointHelper waypointHelper)
        {
            _weatherHelper = weatherHelper;
            _waypointHelper = waypointHelper;
        }

        /// <summary>
        /// Gets the flight details by iata.
        /// </summary>
        /// <param name="flightIata">The flight iata.</param>
        /// <returns></returns>
        public async Task<Flight> GetFlightDetailsByIata(string flightIata)
        {
            string apiKey = "08b1e479905fc25386c758fda85cdcc4";
            string apiUrl =
                $"http://api.aviationstack.com/v1/flights?access_key={apiKey}&flight_iata={flightIata}&flight_status=active";
            var flightDetails = new Flight();

            HttpClient client = new();
            HttpResponseMessage response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                dynamic responseJson = JsonConvert.DeserializeObject(responseData);

                Console.WriteLine("Parsed JSON: " + responseJson);

                flightDetails.Id = responseJson.data[0].flight.iata;
                if (responseJson.data[0].live != null)
                {
                    flightDetails.LastPosition = new Waypoint
                    {
                        Latitude = responseJson.data[0].live.latitude,
                        Longitude = responseJson.data[0].live.longitude,
                        Weather = await _weatherHelper.CalculateScore(
                            responseJson.data[0].live.latitude,
                            responseJson.data[0].live.longitude
                        )
                    };
                }
                flightDetails.Source = responseJson.data[0].departure.iata;
                flightDetails.Destination = responseJson.data[0].arrival.iata;
            }
            return flightDetails;
        }

        /// <summary>
        /// Gets all waypoints of flight.
        /// </summary>
        /// <param name="flightIata">The flight iata.</param>
        /// <returns></returns>
        public async Task<List<Waypoint>> GetAllWaypointsOfFlight(string flightIata)
        {
            // Fetch flight details based on flightIata
            //Flight flightDetails = await GetFlightDetailsByIata(flightIata);
            //if (flightDetails == null)
            //{
            //    return new List<Waypoint>(); // Return empty list if no flight details found
            //}

            // Extract source and destination waypoints from flight details
            // var source = _waypointHelper.GetCoordinatesByIata(flightDetails.Source);
            // var destination = _waypointHelper.GetCoordinatesByIata(flightDetails.Destination);

            var source = new Waypoint { Latitude = 37.7749f, Longitude = -122.4194f };

            var destination = new Waypoint { Latitude = 40.7128f, Longitude = -74.0060f };

            // Calculate waypoints along the great circle path
            var waypoints = _waypointHelper.CalculateGreatCirclePath(source, destination);

            // Optionally, enrich waypoints with weather data
            foreach (var waypoint in waypoints)
            {
                waypoint.Weather = await _weatherHelper.CalculateScore(
                    waypoint.Latitude,
                    waypoint.Longitude
                );
            }

            return waypoints;
        }
    }
}
