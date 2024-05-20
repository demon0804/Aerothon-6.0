namespace Aerothon.Models.Entities
{
    public class Flight
    {
        public string Id { get; set; }
        public Waypoint LastPosition { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
    }
}
