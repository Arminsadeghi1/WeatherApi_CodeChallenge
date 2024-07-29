using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;
using Weather_ApplicationService.Dtos;

namespace Weather_ApplicationService.Weather.ExternalServices;

//ToDo<Armin>(refactor lather): use one abstract generic base service for both services
public class WeatherApiAServise
{
    private readonly ILogger<WeatherApiAServise> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public WeatherApiAServise(
        ILogger<WeatherApiAServise> logger,
        IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<WeatherApiAResponseDto> GetData()
    {

        var loginRequest = new
        {
            user = "X",
            pass = "Y"
        };

        try
        {

            var client = _httpClientFactory.CreateClient("WeatherApiA");

            var loginMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(client.BaseAddress + "v1/login"),
                Content = new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json")
            };

            var loginResponse = await client.SendAsync(loginMessage);
            var accessTocken = await loginResponse.Content.ReadAsStringAsync();

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(accessTocken);

            var dataResponse = await client.GetAsync("v1/current.json?q=London");
            var data = await dataResponse.Content.ReadAsStringAsync();

            if(data is null)
                return null;

            return JsonSerializer.Deserialize<WeatherApiAResponseDto>(data);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }
}
