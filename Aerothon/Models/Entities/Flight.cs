using Aerothon.Models.Response;

namespace Aerothon.Models.Entities
{
    public class Flight
    {
        public string FlightID { get; set; }
        public Waypoint LastPosition { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }

    }
}
