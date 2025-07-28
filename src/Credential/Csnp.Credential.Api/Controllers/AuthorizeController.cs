using Csnp.Credential.Application.Commands.Authorizes.SignIn;
using Csnp.Credential.Application.Commands.Authorizes.SignUp;
using Csnp.Presentation.Common.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Csnp.Credential.Api.Controllers;

/// <summary>
/// AuthorizeController
/// </summary>
public class AuthorizeController : BaseV1Controller
{
    #region -- Methods --

    /// <summary>
    /// Initialize
    /// </summary>
    /// <param name="mediator">Mediator</param>
    public AuthorizeController(ISender mediator) : base(mediator)
    {
    }

    /// <summary>
    /// Sign in with user credentials.
    /// </summary>
    /// <param name="request">User credentials (e.g., email and password).</param>
    /// <returns>Returns authentication result (e.g., JWT token).</returns>
    [HttpPost("sign-in")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Authorize.SignIn.Summary", Description = "Authorize.SignIn.Description")]
    public async Task<IActionResult> SignIn([FromBody] SignInCommand request)
    {
        SignInResponse response = await _mediator.Send(request);
        return Ok(response);
    }

    /// <summary>
    /// Register a new user account (sign up).
    /// </summary>
    /// <param name="request">User registration information.</param>
    /// <returns>Returns the created user or authentication result.</returns>
    [HttpPost("sign-up")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Authorize.SignUp.Summary", Description = "Authorize.SignUp.Description")]
    public async Task<IActionResult> SignUp([FromBody] SignUpCommand request)
    {
        long response = await _mediator.Send(request);
        return Ok(response);
    }

    #endregion
}
