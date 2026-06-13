using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviePlatform.Domain.Common;
using MoviePlatform.Application.Users.Commands.RegisterUser;
using MoviePlatform.Application.Users.Queries.Login;

namespace MoviePlatform.Api.Controllers;

public sealed class UsersController : ApiController
{
	public UsersController(ISender sender) : base(sender) { }

	[HttpPost("register")]
	public async Task<IActionResult> Register([FromBody] RegisterUserCommand command, CancellationToken cancellationToken)
	{
		Result<int> result = await Sender.Send(command, cancellationToken);

		if (result.IsFailure)
		{
			return HandleFailure(result);
		}

		return CreatedAtAction(nameof(Register), new { id = result.Value }, result.Value);
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] LoginQuery query, CancellationToken cancellationToken)
	{
		Result<LoginResponse> result = await Sender.Send(query, cancellationToken);

		if (result.IsFailure)
		{
			return HandleFailure(result);
		}

		return Ok(result.Value);
	}
}
