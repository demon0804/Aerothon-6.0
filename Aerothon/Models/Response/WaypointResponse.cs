namespace Aerothon.Models.Response
{
    public class WaypointResponse
    {
        public float Lattitude { get; set; }
        public float Longitude { get; set; }
        public bool IsSafeToTravel { get; set; }
    }
}
