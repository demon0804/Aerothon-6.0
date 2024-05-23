namespace Aerothon.Models.Response
{
    public class FlightResponse
    {
        public string Id { get; set; }
        public WaypointResponse LastPosition { get; set; }
        public AirportInfoResponse Source { get; set; }
        public AirportInfoResponse Destination { get; set; }
    }
}
