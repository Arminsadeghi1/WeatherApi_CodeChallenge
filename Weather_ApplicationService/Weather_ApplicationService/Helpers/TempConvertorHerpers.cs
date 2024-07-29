namespace Weather_ApplicationService.Helpers;

public static class TempConvertorHerpers
{

    public static double ToCelsius(this double fahrenheitDegree)
    {
        //Formula : (x°F − 32) × 5/9 = 0°C

        return (fahrenheitDegree - 32) * 5.9;
    }
}
