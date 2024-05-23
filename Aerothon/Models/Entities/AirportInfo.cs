namespace Aerothon.Models.Entities
{
    public class AirportInfo
    {
        public string Airport { get; set; }
        public string Timezone { get; set; }
        public string IATA { get; set; }
        public string ICAO { get; set; }
        public DateTime Scheduled { get; set; }
    }
}
