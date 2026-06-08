using MoviePlatform.Domain.Common;
using MoviePlatform.Domain.Movies;

namespace MoviePlatform.Domain.Movies.ValueObjects;

public readonly record struct ReviewScore
{
	public double Value { get; }

	private ReviewScore(double value) => Value = value;

	public static Result<ReviewScore> Create(double value)
	{
		double roundedValue = Math.Round(value, MovieConstants.ReviewScore.DecimalPlacesScale);

		if (roundedValue < MovieConstants.ReviewScore.MinScore || roundedValue > MovieConstants.ReviewScore.MaxScore)
		{
			return Result.Failure<ReviewScore>(MovieErrors.ReviewScore.OutOfRange);
		}

		return Result.Success<ReviewScore>(new(roundedValue));
	}
}
