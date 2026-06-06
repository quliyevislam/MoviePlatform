using MoviePlatform.Domain.Common;
using MoviePlatform.Domain.Movies;

namespace MoviePlatform.Domain.Movies.ValueObjects;

public readonly record struct CommentContent
{
	public string Value { get; }

	private CommentContent(string value) => Value = value;

	public static Result<CommentContent> Create(string value)
	{
			if (value is null)
			{
				return Result.Failure<CommentContent>(MovieErrors.CommentContent.Required);
			}

			string trimmedValue = value.Trim();

			if (value.Length == 0)
			{
				return Result.Failure<CommentContent>(MovieErrors.CommentContent.Empty);
			}

			if (value.Length > MovieConstants.CommentContent.MaxLength)
			{
				return Result.Failure<CommentContent>(MovieErrors.CommentContent.TooLong);
			}

			return Result.Success<CommentContent>(new(value));
	}
}
