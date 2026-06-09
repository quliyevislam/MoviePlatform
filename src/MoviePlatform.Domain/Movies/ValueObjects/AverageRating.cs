using MoviePlatform.Domain.Common;
using MoviePlatform.Domain.Movies;

namespace MoviePlatform.Domain.Movies.ValueObjects;

public readonly record struct AverageRating
{
	public double Value { get; }

	public AverageRating()
	{
		throw new InvalidOperationException("Instantiation via the default parameterless constructor is prohibited.");
	}

	private AverageRating(double value) => Value = value;

	public static Result<AverageRating> Create(double value)
	{

		if (value == 0)
		{
			return Result.Success<AverageRating>(new(value));
		}

		double roundedValue = Math.Round(value, MovieConstants.ReviewScore.DecimalPlacesScale);

		if (roundedValue < MovieConstants.ReviewScore.MinScore || roundedValue > MovieConstants.ReviewScore.MaxScore)
		{
			return Result.Failure<AverageRating>(MovieErrors.AverageRating.OutOfRange);
		}

		return Result.Success<AverageRating>(new(roundedValue));

	}
}
