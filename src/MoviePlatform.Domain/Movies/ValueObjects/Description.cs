using MoviePlatform.Domain.Common;
using MoviePlatform.Domain.Movies;

namespace MoviePlatform.Domain.Movies.ValueObjects;

public readonly record struct Description
{
	public string? Value { get; }

	private Description(string? value) => Value = value;

	public static Result<Description> Create(string? value)
	{
		value = value?.Trim();

		if (value?.Length > MovieConstants.Description.MaxLength)
		{
			return Result.Failure<Description>(MovieErrors.Description.TooLong);
		}

		return Result.Success<Description>(new(value));
	}
}
