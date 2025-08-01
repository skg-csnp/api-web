using Csnp.Presentation.Common.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Csnp.Notification.Api.Controllers;

/// <summary>
/// Provides a sample weather forecast API for demonstration purposes.
/// </summary>
public class WeatherForecastController : BaseV1Controller
{
    #region -- Methods --

    /// <summary>
    /// Initializes a new instance of the <see cref="WeatherForecastController"/> class.
    /// </summary>
    /// <param name="mediator">Mediator</param>
    /// <param name="logger">The logger instance.</param>
    public WeatherForecastController(ISender mediator, ILogger<WeatherForecastController> logger) : base(mediator)
    {
        _logger = logger;
    }

    /// <summary>
    /// Returns a list of random weather forecasts.
    /// </summary>
    /// <returns>An enumerable of <see cref="WeatherForecast"/> objects.</returns>
    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = _summaries[Random.Shared.Next(_summaries.Length)]
        })
        .ToArray();
    }

    #endregion

    #region -- Fields --

    private static readonly string[] _summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild",
        "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    private readonly ILogger<WeatherForecastController> _logger;

    #endregion
}
