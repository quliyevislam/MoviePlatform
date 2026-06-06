using MoviePlatform.Domain.Common;
using MoviePlatform.Domain.Movies.ValueObjects;
using MoviePlatform.Domain.Users.ValueObjects;

namespace MoviePlatform.Domain.Movies.Entities;

public sealed class Comment : BaseEntity<CommentId>
{
	public UserId UserId { get; private set; }
	public MovieId MovieId { get; private set; }
	public CommentContent Content { get; private set; }

	private Comment()
	{
	}

	private Comment(UserId userId, CommentContent content)
	{
		UserId = userId;
		Content = content;
	}

	public static Result<Comment> Create(int userId, string content)
	{
		Result<UserId>	userIdResult = UserId.Create(userId);

		if (userIdResult.IsFailure)
		{
			return Result.Failure<Comment>(userIdResult.Error);
		}

		Result<CommentContent> contentResult = CommentContent.Create(content);

		if (contentResult.IsFailure)
		{
			return Result.Failure<Comment>(contentResult.Error);
		}

		return Result.Success<Comment>(new(userIdResult.Value, contentResult.Value));
	}
}
