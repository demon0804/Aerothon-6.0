using Aerothon.Repository;
using Aerothon.Repository.Interfaces;
using Aerothon.Services;
using Aerothon.Services.Interfaces;
using Microsoft.ML.OnnxRuntime;
using Aerothon.Helper;
using Aerothon.Helper.Interfaces;
using Aerothon.Repository;
using Aerothon.Repository.Interfaces;
using Aerothon.Services;
using Aerothon.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using WeatherApi2._0.Services;
using WeatherApi2._0.Services.Interface;

namespace IMDB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpClient();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WeatherApi2._0", Version = "v1" });
            });
            services.AddSingleton<IFlightRepository, FlightRepository>();
            services.AddSingleton<IFlightService, FlightService>();

            services.AddSingleton<InferenceSession>(
                new InferenceSession("MLModel/weather_safety_model.onnx")
            );
            services.AddSingleton<IWeatherService, WeatherService>();

            services.AddSingleton<IAuthenticationService, AuthenticationService>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IPasswordHelper, PasswordHelper>();
            services.AddSingleton<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WeatherApi2._0 v1")
                );
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
