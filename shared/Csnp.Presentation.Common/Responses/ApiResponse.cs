namespace Csnp.Presentation.Common.Responses;

/// <summary>
/// Represents a standardized API response with generic data support.
/// </summary>
/// <typeparam name="T">The type of the response data.</typeparam>
public class ApiResponse<T>
{
    #region -- Properties --

    /// <summary>
    /// Indicates whether the request was successful.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// The response message providing additional information.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// The data returned from the operation, if any.
    /// </summary>
    public T? Data { get; set; }

    #endregion
}
