using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using WeatherApi2._0.Model;
using WeatherApi2._0.Services.Interface;

namespace WeatherApi2._0.Services
{
    public class WeatherService : IWeatherService
    {
        private InferenceSession _session;

        public WeatherService(InferenceSession session)
        {
            _session = session;
        }

        public string getWeatherPrediction(WeatherParams data)
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
    }
}
