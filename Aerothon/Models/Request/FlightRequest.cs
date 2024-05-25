namespace Aerothon.Models.Request
{
    public class FlightRequest
    {
        public string Id { get; set; }
        public WayPointRequest LastPosition { get; set; }
        public AirportInfoRequest Source { get; set; }
        public AirportInfoRequest Destination { get; set; }
    }
}
