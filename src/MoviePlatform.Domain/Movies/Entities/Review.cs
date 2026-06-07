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

	public static Result<Review> Create(int userId, float score)
	{
		Result<UserId> userIdResult = UserId.Create(userId);

		if (userIdResult.IsFailure)
		{
			return Result.Failure<Review>(userIdResult.Error);
		}

		Result<ReviewScore>	reviewScoreResult = ReviewScore.Create(score);

		if (reviewScoreResult.IsFailure)
		{
			return Result.Failure<Review>(reviewScoreResult.Error);
		}

		return Result.Success<Review>(new(userIdResult.Value, reviewScoreResult.Value));
	}

	internal void UpdateScore(ReviewScore newScore)
	{
		Score = newScore;
	}
}
