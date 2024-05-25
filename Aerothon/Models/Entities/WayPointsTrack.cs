namespace Aerothon.Models.Entities
{
    public class WayPointsTrack
    {
        public string FlightIata { get; set; }
        public List<Waypoint> Waypoints { get; set; }
    }
}
