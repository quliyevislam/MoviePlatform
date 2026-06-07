using MoviePlatform.Domain.Common;
using MoviePlatform.Domain.Movies.ValueObjects;
using MoviePlatform.Domain.Users.ValueObjects;

namespace MoviePlatform.Domain.Movies.Entities;

public sealed class Review : BaseEntity<ReviewId>
{
	public UserId UserId { get; private set; }
	public MovieId MovieId { get; private set; }
	public ReviewScore Score { get; private set; }

	private Review()
	{
	}

	private Review(UserId userId, ReviewScore score)
	{
		UserId = userId;
		Score = score;
	}

	public static Review Create(UserId userId, ReviewScore score)
	{
		return new(userId, score);
	}

	internal void UpdateScore(ReviewScore newScore)
	{
		Score = newScore;
	}
}
