using Aerothon.Models.Entities;

namespace Aerothon.Models.Response
{
    public class WayPointsTrackResponse
    {
        public string FlightId { get; set; }
        public List<WaypointResponse> waypoints { get; set; }
    }
}
