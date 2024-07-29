using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Weather_ApplicationService.Dtos;

namespace Weather_ApplicationService.Weather.ExternalServices;

//ToDo<Armin>(refactor lather): use one abstract generic base service for both services
public class WeatherApiBServise
{
    private readonly ILogger<WeatherApiBServise> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public WeatherApiBServise(
        ILogger<WeatherApiBServise> logger,
        IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<WeatherApiBResponseDto> GetData()
    {

        var loginRequest = new
        {
            user = "X",
            pass = "Y"
        };

        try
        {

            var client = _httpClientFactory.CreateClient("WeatherApiB");

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

            return JsonSerializer.Deserialize<WeatherApiBResponseDto>(data);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return null;
        }
    }
}
