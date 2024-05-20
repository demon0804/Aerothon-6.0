namespace Aerothon.Models.Response
{
    public class FlightResponse
    {
        public string Id { get; set; }
        public WaypointResponse LastPosition { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
    }
}
