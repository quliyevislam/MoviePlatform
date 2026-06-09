using MoviePlatform.Domain.Common;
using MoviePlatform.Domain.Movies.ValueObjects;
using MoviePlatform.Domain.Users.ValueObjects;

namespace MoviePlatform.Domain.Movies.Entities;

public sealed class Review : BaseEntity<ReviewId>
{
	public UserId UserId { get; private set; }
	public MovieId MovieId { get; private set; }
	public ReviewScore Score { get; private set; }
	public DateTime CreatedAtUtc { get; private set; }

	private Review() { }

	private Review(UserId userId, ReviewScore score, DateTime createdAtUtc)
	{
		UserId = userId;
		Score = score;
		CreatedAtUtc = createdAtUtc;
	}

	public static Review Create(UserId userId, ReviewScore score)
	{
		return new(userId, score, DateTime.UtcNow);
	}

	internal void UpdateScore(ReviewScore newScore)
	{
		Score = newScore;
	}
}
