using Aerothon.Helper.Interfaces;
using Aerothon.Models.Entities;
using Aerothon.Repository.Interfaces;
using Newtonsoft.Json;

namespace Aerothon.Repository
{
    public class FlightRepository : IFlightRepository
    {
        private readonly IWeatherHelper _weatherHelper;

        public FlightRepository(IWeatherHelper weatherHelper)
        {
            _weatherHelper = weatherHelper;
        }

        private readonly List<WayPointsTrack> waypointsCollection =
            new()
            {
                new WayPointsTrack
                {
                    FlightIata = "LY4215",
                    Waypoints = new List<Waypoint>
                    {
                        new() { Latitude = 40.7128f, Longitude = -74.0060f, },
                        new() { Latitude = 40.7128f, Longitude = -74.0060f, },
                    }
                }
            };

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
