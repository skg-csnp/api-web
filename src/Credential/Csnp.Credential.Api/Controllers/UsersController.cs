using Csnp.Credential.Application.Commands.Users.CreateUser;
using Csnp.Credential.Application.Queries.Users.GetAllUsers;
using Csnp.Presentation.Common.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Csnp.Credential.Api.Controllers;

/// <summary>
/// UsersController
/// </summary>
public class UsersController : BaseV1Controller
{
    #region -- Methods --

    /// <summary>
    /// Initialize
    /// </summary>
    /// <param name="mediator">Mediator</param>
    public UsersController(ISender mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAll), new { id }, null);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _mediator.Send(new GetAllUsersQuery());
        return Ok(users);
    }

    #endregion
}
