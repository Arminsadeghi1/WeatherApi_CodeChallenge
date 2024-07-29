using Weather_ApplicationService.Weather.ExternalServices;

namespace Weather_ApplicationService.Dtos;
public class WeatherApiAResponseDto
{
    public Coord Coord { get; set; }
    public Weather Weather { get; set; }
}
