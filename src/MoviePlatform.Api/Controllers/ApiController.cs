using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviePlatform.Domain.Common;

namespace MoviePlatform.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiController : ControllerBase
{
	protected readonly ISender Sender;

	protected ApiController(ISender sender)
	{
		Sender = sender;
	}

	protected IActionResult HandleFailure(Result result)
	{
		if (result.IsSuccess)
		{
			throw new InvalidOperationException("Cannot handle a successful result as a failure.");
		}

		return Problem(
			statusCode: result.Error.Type switch
			{
				ErrorType.Failure => StatusCodes.Status422UnprocessableEntity,
				ErrorType.Validation => StatusCodes.Status400BadRequest,
				ErrorType.NotFound => StatusCodes.Status404NotFound,
				ErrorType.Conflict => StatusCodes.Status409Conflict,
				ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
				ErrorType.Forbidden => StatusCodes.Status403Forbidden,
				_ => StatusCodes.Status500InternalServerError
			},
			title: result.Error.Code,
			detail: result.Error.Description
		);
	}
}
