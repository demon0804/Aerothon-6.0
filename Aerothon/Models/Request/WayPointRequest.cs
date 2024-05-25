namespace Aerothon.Models.Request
{
    public class WayPointRequest
    {
        public float Lattitude { get; set; }
        public float Longitude { get; set; }
        public bool IsSafeToTravel { get; set; }
    }
}
