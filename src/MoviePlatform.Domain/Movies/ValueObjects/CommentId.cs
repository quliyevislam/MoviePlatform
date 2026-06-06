using MoviePlatform.Domain.Common;
using MoviePlatform.Domain.Movies;

namespace MoviePlatform.Domain.Movies.ValueObjects;

public readonly record struct CommentId
{
	public int Value { get; }

	private CommentId(int value) => Value = value;

	public static Result<CommentId> Create(int value)
	{
		if (value <= 0)
		{
			return Result.Failure<CommentId>(MovieErrors.CommentId.Invalid);
		}

		return Result.Success<CommentId>(new(value));
	}
}
