using Aerothon.Models.Entities;
using Aerothon.Repository.Interfaces;
using Newtonsoft.Json;

namespace Aerothon.Repository
{
    public class FlightRepository : IFlightRepository
    {
        public FlightRepository() { }

        private readonly List<WayPointsTrack> waypointsCollection =
            new()
            {
                new WayPointsTrack
                {
                    FlightIata = "LY4215",
                    Waypoints = new List<Waypoint>
                    {
                        new()
                        {
                            Latitude = 40.7128f,
                            Longitude = -74.0060f,
                            Weather = "Yes"
                        },
                        new()
                        {
                            Latitude = 40.7128f,
                            Longitude = -74.0060f,
                            Weather = "Yes"
                        },
                    }
                }
            };

        public async Task<Flight> GetFlightDetailsByIata(string flightIata)
        {
            string apiKey = "08b1e479905fc25386c758fda85cdcc4";
            string apiUrl =
                $"http://api.aviationstack.com/v1/flights?access_key={apiKey}&flight_iata={flightIata}&flight_status=active";
            var flightDetails = new Flight();

            try
            {
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
                            Weather = responseJson.data[0].live.weather ?? "Unknown"
                        };
                    }
                    flightDetails.Source = responseJson.data[0].departure.iata;
                    flightDetails.Destination = responseJson.data[0].arrival.iata;
                }
                return flightDetails;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<Waypoint> GetAllWaypointsOfFlight(string flightIata)
        {
            var waypointsTrack = waypointsCollection.FirstOrDefault(f =>
                f.FlightIata == flightIata
            );
            return waypointsTrack.Waypoints.ToList();
        }
    }
}
