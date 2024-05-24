using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WeatherApi2._0.Model;
using WeatherApi2._0.Services.Interface;

[ApiController]
[Route("/[controller]")]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;
    private readonly IHttpClientFactory _httpClientFactory;

    public WeatherController(IWeatherService weatherService, IHttpClientFactory httpClientFactory)
    {
        _weatherService = weatherService;
        _httpClientFactory = httpClientFactory;
    }

    [HttpPost("score")]
    public async Task<ActionResult<Prediction>> Score(
        [FromQuery] float latitude,
        [FromQuery] float longitude
    )
    {
        try
        {
            string apiKey = "78483b9825ceeeeb6c1d9440afb18f23";
            string apiUrl =
                $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={apiKey}";

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(apiUrl);
            WeatherParams weatherParams = new WeatherParams();

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                dynamic responseJson = JsonConvert.DeserializeObject(responseData);

                weatherParams.Temperature = responseJson.main.temp;
                weatherParams.ApparentTemperature = responseJson.main.feels_like;
                weatherParams.Humidity = responseJson.main.humidity;
                weatherParams.WindSpeed = responseJson.wind.speed;
                weatherParams.Visibility = responseJson.visibility;
                weatherParams.Pressure = responseJson.main.pressure;

                // Check if "Rain" exists in the "weather" array
                bool rainExists = false;
                foreach (var item in responseJson.weather)
                {
                    if (item.main == "Rain")
                    {
                        rainExists = true;
                        break;
                    }
                }

                // Set weatherParams.Rain based on the result
                weatherParams.Rain = rainExists ? 1 : 0;
            }
            return Ok(_weatherService.getWeatherPrediction(weatherParams));
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
    public ActionResult<Prediction> Score([FromBody] WeatherParams data)
    {

        var predictedList = _weatherService.getWeatherPrediction(data); // Call the Score method
        return Ok(predictedList);


    }
}
