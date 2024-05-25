namespace Aerothon.Models.Entities
{
    /// <summary>
    /// Flight
    /// </summary>
    public class Flight
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the last position.
        /// </summary>
        /// <value>
        /// The last position.
        /// </value>
        public Waypoint LastPosition { get; set; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public AirportInfo Source { get; set; }

        /// <summary>
        /// Gets or sets the destination.
        /// </summary>
        /// <value>
        /// The destination.
        /// </value>
        public AirportInfo Destination { get; set; }
    }
}
