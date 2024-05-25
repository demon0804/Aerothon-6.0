using Aerothon.Helper.Interfaces;
using Aerothon.Models.Entities;

namespace Aerothon.Helper
{
    /// <summary>
    /// WayPoint helper
    /// </summary>
    public class WaypointHelper
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }

        /// <summary>
        /// Waypoint helper
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        public WaypointHelper(float latitude, float longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        /// <summary>
        /// Distance to waypoint
        /// </summary>
        /// <param name="other">destination</param>
        /// <returns>distance</returns>
        public double DistanceTo(Waypoint other)
        {
            const double R = 6371; // Radius of the Earth in kilometers
            var lat1 = ToRadians(Latitude);
            var lon1 = ToRadians(Longitude);
            var lat2 = ToRadians(other.Latitude);
            var lon2 = ToRadians(other.Longitude);

            var dlat = lat2 - lat1;
            var dlon = lon2 - lon1;

            var a =
                Math.Sin(dlat / 2) * Math.Sin(dlat / 2)
                + Math.Cos(lat1) * Math.Cos(lat2) * Math.Sin(dlon / 2) * Math.Sin(dlon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var distance = R * c;

            return distance;
        }

        private double ToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }
    }
}
