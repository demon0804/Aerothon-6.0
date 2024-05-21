namespace Aerothon.Helper.Interfaces
{
    public interface IWeatherHelper
    {
        /// <summary>
        /// Calculates the score.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <returns></returns>
        Task<string> CalculateScore(float latitude, float longitude);
    }
}
