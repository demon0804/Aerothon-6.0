namespace Aerothon.Helper.Interfaces
{
    public interface IWeatherHelper
    {
        Task<string> CalculateScore(float latitude, float longitude);
    }
}
