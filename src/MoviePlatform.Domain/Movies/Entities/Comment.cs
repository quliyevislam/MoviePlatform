using MoviePlatform.Domain.Common;
using MoviePlatform.Domain.Movies.ValueObjects;
using MoviePlatform.Domain.Users.ValueObjects;

namespace MoviePlatform.Domain.Movies.Entities;

public sealed class Comment : BaseEntity<CommentId>
{
	public UserId UserId { get; private set; }
	public MovieId MovieId { get; private set; }
	public CommentContent Content { get; private set; }
	public DateTime CreatedAtUtc { get; private set; }

	private Comment() { }

	private Comment(UserId userId, CommentContent content, DateTime createdAtUtc)
	{
		UserId = userId;
		Content = content;
		CreatedAtUtc = createdAtUtc;
	}

	public static Result<Comment> Create(int userId, string? content)
	{
		Result<UserId> userIdResult = UserId.Create(userId);

		if (userIdResult.IsFailure)
		{
			return Result.Failure<Comment>(userIdResult.Error);
		}

		Result<CommentContent> commentContentResult = CommentContent.Create(content);

		if (commentContentResult.IsFailure)
		{
			return Result.Failure<Comment>(commentContentResult.Error);
		}

		return Result.Success<Comment>(new(userIdResult.Value, commentContentResult.Value, DateTime.UtcNow));
	}
}
