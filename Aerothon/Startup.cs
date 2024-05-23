using Aerothon.Helper;
using Aerothon.Helper.Interfaces;
using Aerothon.Repository;
using Aerothon.Repository.Interfaces;
using Aerothon.Services;
using Aerothon.Services.Interfaces;
using Microsoft.ML.OnnxRuntime;
using Microsoft.OpenApi.Models;

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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AerothonWebApp", Version = "v1" });
            });

            services.AddSingleton<InferenceSession>(
                new InferenceSession("MLModel/weather_safety_model.onnx")
            );

            services.AddSingleton<IFlightRepository, FlightRepository>();
            services.AddSingleton<IFlightService, FlightService>();
            services.AddSingleton<IWeatherHelper, WeatherHelper>();
            services.AddSingleton<IWaypointHelper, WaypointHelper>();

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
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AerothonWebApp v1")
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
