namespace Aerothon.Models.Entities
{
    public class WeatherParams
    {
        public float Rain { get; set; }
        public float Temperature { get; set; }
        public float ApparentTemperature { get; set; }
        public float Humidity { get; set; }
        public float WindSpeed { get; set; }
        public float WindBearing { get; set; }
        public float Visibility { get; set; }
        public float Pressure { get; set; }
    }

    public class Prediction
    {
        public List<string> PredictedValue { get; set; }
    }
}
