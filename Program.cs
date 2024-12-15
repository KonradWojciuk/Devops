using Microsoft.Extensions.DependencyInjection;
using WeatherApp.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddSingleton<WeatherService>();

var app = builder.Build();

// Config app to listen on all interfaces
app.Urls.Add("http://0.0.0.0:5000");

app.MapGet("/weather/{city}", async (string city, WeatherService weatherService) =>
{
    var weather = await weatherService.GetWeatherAsync(city);
    return weather is not null ? Results.Ok(weather) : Results.NotFound();
});

app.UseStaticFiles();

app.Run();
