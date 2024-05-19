namespace Aerothon.Models.Entities
{
    public class WayPointsTrack
    {
        public string flightId { get; set; }
        public List<Waypoint> waypoints { get; set; }
    }
}
