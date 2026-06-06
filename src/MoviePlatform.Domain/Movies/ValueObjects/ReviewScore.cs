using MoviePlatform.Domain.Common;
using MoviePlatform.Domain.Movies;

namespace MoviePlatform.Domain.Movies.ValueObjects;

public readonly record struct ReviewScore
{
	public float Value { get; }

	private ReviewScore(float value) => Value = value;

	public static Result<ReviewScore> Create(float value)
	{
		if (value < MovieConstants.ReviewScore.MinScore || value > MovieConstants.ReviewScore.MaxScore)
		{
			return Result.Failure<ReviewScore>(MovieErrors.ReviewScore.OutOfRange);
		}

		return Result.Success<ReviewScore>(new(value));
	}
}
