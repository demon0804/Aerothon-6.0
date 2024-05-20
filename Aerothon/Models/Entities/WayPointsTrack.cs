namespace Aerothon.Models.Entities
{
    public class WayPointsTrack
    {
        public string FlightId { get; set; }
        public List<Waypoint> waypoints { get; set; }
    }
}
