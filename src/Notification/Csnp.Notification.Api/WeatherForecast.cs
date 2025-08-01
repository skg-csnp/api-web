namespace Csnp.Notification.Api;

/// <summary>
/// Represents a weather forecast data model used for demo or testing purposes.
/// </summary>
public class WeatherForecast
{
    #region -- Properties --

    /// <summary>
    /// Gets or sets the forecast date.
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Gets or sets the temperature in Celsius.
    /// </summary>
    public int TemperatureC { get; set; }

    /// <summary>
    /// Gets the temperature in Fahrenheit.
    /// </summary>
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    /// <summary>
    /// Gets or sets the summary description of the weather.
    /// </summary>
    public string? Summary { get; set; }

    #endregion
}
