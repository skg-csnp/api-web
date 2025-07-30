using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Csnp.Presentation.Common.Controllers;

/// <summary>
/// Provides a common base class for all version 1 API controllers.
/// Includes shared functionality such as access to the <see cref="ISender"/> (MediatR).
/// </summary>
[ApiController]
[Route("v1/[controller]")]
[ApiExplorerSettings(GroupName = "v1")]
public abstract class BaseV1Controller : ControllerBase
{
    #region -- Methods --

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseV1Controller"/> class with the specified <see cref="ISender"/>.
    /// </summary>
    /// <param name="mediator">The MediatR <see cref="ISender"/> instance used for sending commands and queries.</param>
    protected BaseV1Controller(ISender mediator)
    {
        _mediator = mediator;
    }

    #endregion

    #region -- Fields --

    /// <summary>
    /// The MediatR sender used to send commands or queries.
    /// </summary>
    protected readonly ISender _mediator;

    #endregion
}
