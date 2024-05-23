namespace Aerothon.Helper.Interfaces
{
    /// <summary>
    /// IWeatherHelper
    /// </summary>
    public interface IWeatherHelper
    {
        /// <summary>
        /// Calculates the score.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <returns></returns>
        Task<bool> CheckWeatherIsSafeToTravel(float latitude, float longitude);
    }
}
