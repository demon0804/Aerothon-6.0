namespace Aerothon.Models.Entities
{
    /// <summary>
    /// Weather Parameters
    /// </summary>
    public class WeatherParams
    {
        /// <summary>
        /// Gets or sets the rain.
        /// </summary>
        /// <value>
        /// The rain.
        /// </value>
        public float Rain { get; set; }

        /// <summary>
        /// Gets or sets the temperature.
        /// </summary>
        /// <value>
        /// The temperature.
        /// </value>
        public float Temperature { get; set; }

        /// <summary>
        /// Gets or sets the apparent temperature.
        /// </summary>
        /// <value>
        /// The apparent temperature.
        /// </value>
        public float ApparentTemperature { get; set; }

        /// <summary>
        /// Gets or sets the humidity.
        /// </summary>
        /// <value>
        /// The humidity.
        /// </value>
        public float Humidity { get; set; }

        /// <summary>
        /// Gets or sets the wind speed.
        /// </summary>
        /// <value>
        /// The wind speed.
        /// </value>
        public float WindSpeed { get; set; }

        /// <summary>
        /// Gets or sets the wind bearing.
        /// </summary>
        /// <value>
        /// The wind bearing.
        /// </value>
        public float WindBearing { get; set; }

        /// <summary>
        /// Gets or sets the visibility.
        /// </summary>
        /// <value>
        /// The visibility.
        /// </value>
        public float Visibility { get; set; }

        /// <summary>
        /// Gets or sets the pressure.
        /// </summary>
        /// <value>
        /// The pressure.
        /// </value>
        public float Pressure { get; set; }
    }
}
