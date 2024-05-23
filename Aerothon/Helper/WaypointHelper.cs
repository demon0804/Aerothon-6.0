using Aerothon.Models.Entities;
using Newtonsoft.Json;

namespace Aerothon.Helper
{
    /// <summary>
    /// WaypointHelper
    /// </summary>
    /// <seealso cref="Aerothon.Helper.IWaypointHelper" />
    public class WaypointHelper : IWaypointHelper
    {
        public WaypointHelper() { }

        /// <summary>
        /// Calculates the great circle path.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <returns></returns>
        public List<Waypoint> CalculateWayPoints(Waypoint source, Waypoint destination)
        {
            // var waypoints = new List<Waypoint>();
            // double d = CalculateDistance(source, destination);
            // int steps = (int)(d / 100); // Assuming waypoints every 100 km

            // for (int i = 1; i < steps; i++)
            // {
            //     double fraction = (double)i / steps;
            //     waypoints.Add(IntermediatePoint(source, destination, fraction));
            // }

            // return waypoints;

            int numberOfPoints = 5;
            List<Waypoint> points = new();

            for (int i = 0; i <= numberOfPoints; i++)
            {
                float latitude =
                    source.Latitude + (destination.Latitude - source.Latitude) * i / numberOfPoints;
                float longitude =
                    source.Longitude
                    + (destination.Longitude - source.Longitude) * i / numberOfPoints;
                points.Add(new Waypoint { Latitude = latitude, Longitude = longitude });
            }

            return points;
        }

        /// <summary>
        /// Gets the coordinates by iata.
        /// </summary>
        /// <param name="iata"></param>
        /// <returns></returns>
        public async Task<Waypoint> GetCoordinatesByAirport(string airport)
        {
            string apiKey = "2d854a35116a4ee9a0e8b318b3b5dfd6";
            string apiUrl =
                $"https://api.geoapify.com/v1/geocode/search?text={airport}&apiKey={apiKey}";
            var waypoint = new Waypoint();

            HttpClient client = new();
            HttpResponseMessage response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                dynamic responseJson = JsonConvert.DeserializeObject(responseData);

                waypoint.Longitude = responseJson.features[0].properties.lon;
                waypoint.Latitude = responseJson.features[0].properties.lat;
            }

            return waypoint;
        }

        // /// <summary>
        // /// Calculates intermediate points.
        // /// </summary>
        // /// <param name="start">The start.</param>
        // /// <param name="end">The end.</param>
        // /// <param name="fraction">The fraction.</param>
        // /// <returns></returns>
        // public static Waypoint IntermediatePoint(Waypoint start, Waypoint end, double fraction)
        // {
        //     double lat1 = DegreesToRadians(start.Latitude);
        //     double lon1 = DegreesToRadians(start.Longitude);
        //     double lat2 = DegreesToRadians(end.Latitude);
        //     double lon2 = DegreesToRadians(end.Longitude);

        //     double dLon = lon2 - lon1;

        //     double Bx = Math.Cos(lat2) * Math.Cos(dLon);
        //     double By = Math.Cos(lat2) * Math.Sin(dLon);
        //     double lat3 = Math.Atan2(
        //         Math.Sin(lat1) + Math.Sin(lat2),
        //         Math.Sqrt((Math.Cos(lat1) + Bx) * (Math.Cos(lat1) + Bx) + By * By)
        //     );
        //     double lon3 = lon1 + Math.Atan2(By, Math.Cos(lat1) + Bx);

        //     return new Waypoint
        //     {
        //         Latitude = (float)RadiansToDegrees(lat3),
        //         Longitude = (float)RadiansToDegrees(lon3)
        //     };
        // }

        // /// <summary>
        // /// Degreeses to radians.
        // /// </summary>
        // /// <param name="degrees">The degrees.</param>
        // /// <returns></returns>
        // private static double DegreesToRadians(double degrees)
        // {
        //     return degrees * Math.PI / 180.0;
        // }

        // /// <summary>
        // /// Radianses to degrees.
        // /// </summary>
        // /// <param name="radians">The radians.</param>
        // /// <returns></returns>
        // private static double RadiansToDegrees(double radians)
        // {
        //     return radians * 180.0 / Math.PI;
        // }

        // /// <summary>
        // /// Calculates the distance.
        // /// </summary>
        // /// <param name="start">The start.</param>
        // /// <param name="end">The end.</param>
        // /// <returns></returns>
        // private static double CalculateDistance(Waypoint start, Waypoint end)
        // {
        //     double lat1 = DegreesToRadians(start.Latitude);
        //     double lon1 = DegreesToRadians(start.Longitude);
        //     double lat2 = DegreesToRadians(end.Latitude);
        //     double lon2 = DegreesToRadians(end.Longitude);

        //     double dLat = lat2 - lat1;
        //     double dLon = lon2 - lon1;

        //     double a =
        //         Math.Sin(dLat / 2) * Math.Sin(dLat / 2)
        //         + Math.Cos(lat1) * Math.Cos(lat2) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
        //     double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        //     double R = 6371; // Earth's radius in kilometers
        //     return R * c;
        // }
    }
}
