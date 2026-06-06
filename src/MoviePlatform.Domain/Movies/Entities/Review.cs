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

		Result<ReviewScore>	scoreResult = ReviewScore.Create(score);

		if (scoreResult.IsFailure)
		{
			return Result.Failure<Review>(scoreResult.Error);
		}

		return Result.Success<Review>(new(userIdResult.Value, scoreResult.Value));
	}
}
