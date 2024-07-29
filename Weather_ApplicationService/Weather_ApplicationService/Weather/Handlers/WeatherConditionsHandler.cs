using Weather_ApplicationService.Helpers;
using Weather_ApplicationService.Weather.ExternalServices;

namespace Weather_ApplicationService.Weather.Handlers;

public class WeatherConditionsHandler
{
    private readonly WeatherApiAServise _weatherApiAServise;
    private readonly WeatherApiBServise _weatherApiBServise;

    public WeatherConditionsHandler(
        WeatherApiAServise weatherApiAServise,
        WeatherApiBServise weatherApiBServise
        )
    {
        _weatherApiAServise = weatherApiAServise;
        _weatherApiBServise = weatherApiBServise;
    }


    public async Task<double> Handle(CancellationToken cancellationToken)
    {

        var resultA = await _weatherApiAServise.GetData();

        if (resultA != null)
        {
            return resultA.Weather.Celsius;
        }

        var resultB = await _weatherApiBServise.GetData();

        if (resultB != null)
        {
            return resultB.Fahrenheit.ToCelsius();
        }

        throw new NotSupportedException();
    }
}
