using System.Net;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IWeatherClient, WeatherClient>();
builder.Services.AddTransient<IWeatherService, WeatherService>();

var app = builder.Build();
app.MapGet("/weather/city/{cityName}", (string cityName, IWeatherService weatherService, ILogger<Program> logger) =>
{
    logger.LogInformation("Fetching weather info for city: {CityName}", cityName);
    string ? weatherInfo = weatherService.GetWeatherInfo(cityName);
    logger.LogInformation("Fetched weather info for city: {CityName}", cityName);
    return Results.Ok(weatherInfo);
});

app.Run();

interface IWeatherService
{
    string GetWeatherInfo(string cityName);
}

class WeatherService : IWeatherService
{
    private IWeatherClient _weatherClient;

    public WeatherService(IWeatherClient weatherClient)
    {
        _weatherClient = weatherClient;
    }

    public string GetWeatherInfo(string cityName)
    {
        return _weatherClient.FetchWeatherData(cityName);
    }
}

interface IWeatherClient
{
    string FetchWeatherData(string cityName);
}

class WeatherClient : IWeatherClient
{

    public string FetchWeatherData(string cityName)
    {
        // Replace this stub with a real HTTP/API call to fetch weather data.
        return $"Weather for {cityName}: {Random.Shared.Next(-10, 40)} °C";
    }
}