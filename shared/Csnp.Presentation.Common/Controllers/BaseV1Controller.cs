using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Csnp.Presentation.Common.Controllers;

/// <summary>
/// BaseV1Controller
/// </summary>
[ApiController]
[Route("v1/[controller]")]
[ApiExplorerSettings(GroupName = "v1")]
public abstract class BaseV1Controller : ControllerBase
{
    #region -- Methods --

    /// <summary>
    /// Initialize
    /// </summary>
    /// <param name="mediator">Mediator</param>
    protected BaseV1Controller(ISender mediator)
    {
        _mediator = mediator;
    }

    #endregion

    #region -- Fields --

    /// <summary>
    /// Mediator
    /// </summary>
    protected readonly ISender _mediator;

    #endregion
}
