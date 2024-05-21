namespace Aerothon.Models.Entities
{
    /// <summary>
    /// Waypoint
    /// </summary>
    public class Waypoint
    {
        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        public float Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        public float Longitude { get; set; }

        /// <summary>
        /// Gets or sets the weather.
        /// </summary>
        /// <value>
        /// The weather.
        /// </value>
        public string Weather { get; set; }
    }
}
