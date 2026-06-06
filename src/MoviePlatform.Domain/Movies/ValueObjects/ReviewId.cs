using MoviePlatform.Domain.Common;
using MoviePlatform.Domain.Movies;

namespace MoviePlatform.Domain.Movies.ValueObjects;

public readonly record struct ReviewId
{
	public int Value { get; }

	private ReviewId(int value) => Value = value;

	public static Result<ReviewId> Create(int value)
	{
		if (value <= 0)
		{
			return Result.Failure<ReviewId>(MovieErrors.ReviewId.Invalid);
		}

		return Result.Success<ReviewId>(new(value));
	}
}
