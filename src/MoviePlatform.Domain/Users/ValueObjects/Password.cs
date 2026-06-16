using MoviePlatform.Domain.Common;
using MoviePlatform.Domain.Users;
using System.Text.RegularExpressions;

namespace MoviePlatform.Domain.Users.ValueObjects;

public readonly record struct Password
{
	public string Value { get; }

	private Password(string value) => Value = value;

	public static Result<Password> Create(string? value)
	{
		if (value is null)
		{
			return Result.Failure<Password>(UserErrors.Password.Required);
		}

		if (value.Length == 0)
		{
			return Result.Failure<Password>(UserErrors.Password.Empty);
		}

		if (!Regex.IsMatch(value, UserConstants.Password.RequireNoSpace))
		{
			return Result.Failure<Password>(UserErrors.Password.SpacesNotAllowed);
		}

		if (value.Length < UserConstants.Password.MinLength)
		{
			return Result.Failure<Password>(UserErrors.Password.TooShort);
		}

		if (!Regex.IsMatch(value, UserConstants.Password.RequireUppercase))
		{
			return Result.Failure<Password>(UserErrors.Password.UppercaseRequired);
		}

		if (!Regex.IsMatch(value, UserConstants.Password.RequireLowercase))
		{
			return Result.Failure<Password>(UserErrors.Password.LowercaseRequired);
		}

		if (!Regex.IsMatch(value, UserConstants.Password.RequireDigit))
		{
			return Result.Failure<Password>(UserErrors.Password.DigitRequired);
		}

		return Result.Success<Password>(new(value));
	}
}
