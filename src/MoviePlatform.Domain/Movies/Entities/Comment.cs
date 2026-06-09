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

	public static Comment Create(UserId userId, CommentContent content)
	{
		return new(userId, content, DateTime.UtcNow);
	}
}
