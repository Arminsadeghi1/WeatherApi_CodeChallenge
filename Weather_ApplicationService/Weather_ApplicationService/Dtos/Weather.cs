﻿namespace Weather_ApplicationService.Dtos;

public class Weather
{
    public long Id { get; set; }
    public string Main { get; set; }
    public string Description { get; set; }
    public string Icon { get; set; }
    public double Celsius { get; set; }
}