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

	public static Comment Create(UserId userId, CommentContent content)
	{
		return new(userId, content);
	}
}
