using Aerothon.Helper.Interfaces;
using Aerothon.Models.Entities;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using Newtonsoft.Json;

namespace Aerothon.Helper
{
    public class WeatherHelper : IWeatherHelper
    {
        /// <summary>
        /// The session
        /// </summary>
        private readonly InferenceSession _session;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeatherHelper"/> class.
        /// </summary>
        /// <param name="session">The session.</param>
        public WeatherHelper(InferenceSession session)
        {
            _session = session;
        }

        /// <summary>
        /// Calculates the score.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <returns></returns>
        public async Task<string> CalculateScore(float latitude, float longitude)
        {
            WeatherParams weatherParams = await GetWeatherParams(latitude, longitude);
            return GetWeatherPrediction(weatherParams);
        }

        /// <summary>
        /// Gets the weather prediction.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        private string GetWeatherPrediction(WeatherParams data)
        {
            var input = new DenseTensor<float>(
                new[]
                {
                    (float)data.Rain,
                    data.Temperature,
                    data.ApparentTemperature,
                    data.Humidity,
                    data.WindSpeed,
                    data.WindBearing,
                    data.Visibility,
                    data.Pressure
                },
                new[] { 1, 8 }
            );

            // Create a list of NamedOnnxValue for input
            var inputs = new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("float_input", input)
            };

            // Run the model
            using var results = _session.Run(inputs);

            // Get the prediction result from the first item in the results collection
            var prediction = results[0].Value;

            // Return the prediction result
            var denseTensor = (DenseTensor<string>)prediction; // Cast prediction to DenseTensor<string>

            // Extract the string values from the tensor and convert them to a list
            var predictedList = denseTensor.ToArray().ToList()[0];

            return predictedList;
        }

        /// <summary>
        /// Gets the weather parameters.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <returns></returns>
        private async static Task<WeatherParams> GetWeatherParams(float latitude, float longitude)
        {
            string apiKey = "78483b9825ceeeeb6c1d9440afb18f23";
            string apiUrl =
                $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={apiKey}&units=metric";

            HttpClient client = new();
            HttpResponseMessage response = await client.GetAsync(apiUrl);
            WeatherParams weatherParams = new();

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

            return weatherParams;
        }
    }
}
