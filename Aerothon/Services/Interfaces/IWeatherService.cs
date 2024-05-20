using WeatherApi2._0.Model;

namespace WeatherApi2._0.Services.Interface
{
    public interface IWeatherService
    {
        string getWeatherPrediction(WeatherParams data);
    }
}
