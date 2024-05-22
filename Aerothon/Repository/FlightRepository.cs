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

        /// <summary>
        /// Initializes a new instance of the <see cref="FlightRepository"/> class.
        /// </summary>
        /// <param name="weatherHelper">The weather helper.</param>
        public FlightRepository(IWeatherHelper weatherHelper)
        {
            _weatherHelper = weatherHelper;
        }

        /// <summary>
        /// The waypoints collection (static list)
        /// TODO: Implement this using API
        /// </summary>
        private readonly List<WayPointsTrack> waypointsCollection =
            new()
            {
                new WayPointsTrack
                {
                    FlightIata = "LY4215",
                    Waypoints = new List<Waypoint>
                    {
                        new() { Latitude = -41.2865f, Longitude = 174.7762f },
                        new() { Latitude = 41.8781f, Longitude = -87.6298f },
                        new() { Latitude = 64.1355f, Longitude = -21.8954f },
                        new() { Latitude = 55.9533f, Longitude = -3.1883f },
                        new() { Latitude = -33.9249f, Longitude = 18.4241f },
                        new() { Latitude = -42.8821f, Longitude = 147.3272f },
                        new() { Latitude = 40.7128f, Longitude = -10.0060f },
                        new() { Latitude = 40.7128f, Longitude = -74.0060f },
                        new() { Latitude = 48.8566f, Longitude = 2.3522f }, // Paris, France
                        new() { Latitude = 35.6895f, Longitude = 139.6917f }, // Tokyo, Japan
                        new() { Latitude = -34.6037f, Longitude = -58.3816f }, // Buenos Aires, Argentina
                        new() { Latitude = 51.5074f, Longitude = -0.1278f }, // London, UK
                        new() { Latitude = 37.7749f, Longitude = -122.4194f }, // San Francisco, USA
                        new() { Latitude = -22.9068f, Longitude = -43.1729f }, // Rio de Janeiro, Brazil
                        new() { Latitude = 34.0522f, Longitude = -118.2437f }, // Los Angeles, USA
                        new() { Latitude = 28.6139f, Longitude = 77.2090f }, // New Delhi, India
                        new() { Latitude = -1.2921f, Longitude = 36.8219f }, // Nairobi, Kenya
                        new() { Latitude = 55.7558f, Longitude = 37.6173f }
                    }
                }
            };

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
            var waypointsTrack = waypointsCollection.FirstOrDefault(f =>
                f.FlightIata == flightIata
            );
            if (waypointsTrack == null)
            {
                return new List<Waypoint>();
            }
            var result = waypointsTrack.Waypoints.ToList();
            for (int i = 0; i < result.Count; i++)
            {
                result[i].Weather = await _weatherHelper.CalculateScore(
                    result[i].Latitude,
                    result[i].Longitude
                );
            }
            return result;
        }
    }
}
