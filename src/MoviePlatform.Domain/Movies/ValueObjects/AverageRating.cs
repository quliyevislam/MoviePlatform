using MoviePlatform.Domain.Common;
using MoviePlatform.Domain.Movies;

namespace MoviePlatform.Domain.Movies.ValueObjects;

public readonly record struct AverageRating
{
	public float Value { get; }

	private AverageRating(float value) => Value = value;

	public static Result<AverageRating> Create(float value)
	{
		if (value < MovieConstants.ReviewScore.MinScore || value > MovieConstants.ReviewScore.MaxScore)
		{
			return Result.Failure<AverageRating>(MovieErrors.AverageRating.OutOfRange);
		}

		return Result.Success<AverageRating>(new(value));
	}
}
