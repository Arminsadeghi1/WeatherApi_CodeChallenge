using Microsoft.Net.Http.Headers;
using Weather_ApplicationService.Weather.ExternalServices;
using Weather_ApplicationService.Weather.Handlers;

namespace WeatherApi_CodeChallenge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add Handlers
            builder.Services.AddScoped<WeatherConditionsHandler>();

            //Add Servises
            builder.Services.AddScoped<WeatherApiAServise>();
            builder.Services.AddScoped<WeatherApiBServise>();

            //Add Clients
            builder.Services.AddHttpClient("WeatherApiA", httpClient =>
            {
                httpClient.BaseAddress = new Uri(builder.Configuration["ExternalServices:WeatherApiAUrl"]);
            });
            builder.Services.AddHttpClient("WeatherApiB", httpClient =>
            {
                httpClient.BaseAddress = new Uri(builder.Configuration["ExternalServices:WeatherApiBUrl"]);
            });

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}