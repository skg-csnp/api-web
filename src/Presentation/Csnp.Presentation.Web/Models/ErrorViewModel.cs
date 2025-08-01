namespace Csnp.Presentation.Web.Models;

/// <summary>
/// View model representing error details for display in error views.
/// </summary>
public class ErrorViewModel
{
    #region -- Properties --

    /// <summary>
    /// The unique identifier for the current request.
    /// </summary>
    public string? RequestId { get; set; }

    /// <summary>
    /// Indicates whether the <see cref="RequestId"/> should be displayed.
    /// </summary>
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    #endregion
}
