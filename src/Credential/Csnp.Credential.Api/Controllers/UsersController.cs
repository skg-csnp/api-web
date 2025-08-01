using Csnp.Credential.Application.Commands.Users.CreateUser;
using Csnp.Credential.Application.Queries.Users.Dtos;
using Csnp.Credential.Application.Queries.Users.GetAllUsers;
using Csnp.Presentation.Common.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Csnp.Credential.Api.Controllers;

/// <summary>
/// Handles user-related API endpoints.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class UsersController : BaseV1Controller
{
    #region -- Methods --

    /// <summary>
    /// Initializes a new instance of the <see cref="UsersController"/> class.
    /// </summary>
    /// <param name="mediator">The mediator for dispatching commands and queries.</param>
    public UsersController(ISender mediator) : base(mediator)
    {
    }

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="command">The user creation command.</param>
    /// <returns>The result of the operation.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        long id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAll), new { id }, null);
    }

    /// <summary>
    /// Retrieves all users.
    /// </summary>
    /// <returns>A list of users.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        IEnumerable<UserDto> users = await _mediator.Send(new GetAllUsersQuery());
        return Ok(users);
    }

    #endregion
}
