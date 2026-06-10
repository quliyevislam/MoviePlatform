using MoviePlatform.Domain.Common;
using MoviePlatform.Domain.Movies;

namespace MoviePlatform.Domain.Movies.ValueObjects;

public readonly record struct MovieId
{
	public int Value { get; }

	public MovieId()
	{
		throw new InvalidOperationException("Instantiation via the default parameterless constructor is prohibited.");
	}

	private MovieId(int value) => Value = value;

	public static Result<MovieId> Create(int value)
	{
		// if (value <= 0)
		// {
		//		return Result.Failure<MovieId>(MovieErrors.MovieId.Invalid);
		// }

		return Result.Success<MovieId>(new(value));
	}
}
