namespace Weather_ApplicationService.Dtos;

public class WeatherApiBResponseDto
{
    public double Lon { get; set; }
    public double Lat { get; set; }
    public long Id { get; set; }
    public string Main { get; set; }
    public string Description { get; set; }
    public string Icon { get; set; }
    public double Fahrenheit { get; set; }
}
