namespace Csnp.Presentation.Common.Responses;

/// <summary>
/// Provides factory methods for creating instances of <see cref="ApiResponse{T}"/>.
/// </summary>
public static class ApiResponseFactory
{
    #region -- Methods --

    /// <summary>
    /// Creates a successful <see cref="ApiResponse{T}"/> with the specified data and optional message.
    /// </summary>
    /// <typeparam name="T">The type of the response data.</typeparam>
    /// <param name="data">The response payload.</param>
    /// <param name="message">The optional success message. Default is "Success".</param>
    /// <returns>An instance of <see cref="ApiResponse{T}"/> indicating success.</returns>
    public static ApiResponse<T> Ok<T>(T data, string message = "Success")
    {
        return new ApiResponse<T>
        {
            Success = true,
            Message = message,
            Data = data
        };
    }

    /// <summary>
    /// Creates a failed <see cref="ApiResponse{T}"/> with the specified error message.
    /// </summary>
    /// <typeparam name="T">The type of the response data.</typeparam>
    /// <param name="message">The error message.</param>
    /// <returns>An instance of <see cref="ApiResponse{T}"/> indicating failure.</returns>
    public static ApiResponse<T> Fail<T>(string message)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Message = message,
            Data = default
        };
    }

    #endregion
}
