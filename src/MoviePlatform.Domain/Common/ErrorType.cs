namespace MoviePlatform.Domain.Common;

public enum ErrorType
{
	None,
	Failure,
	Validation,
	NotFound,
	Conflict,
	Unauthorized,
	Forbidden
}
