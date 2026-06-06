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

	private Review(UserId userId, MovieId movieId, ReviewScore score)
	{
		UserId = userId;
		MovieId = movieId;
		Score = score;
	}

	public static Result<Review> Create(int userId, int movieId, float score)
	{
		Result<UserId> userIdResult = UserId.Create(userId);

		if (userIdResult.IsFailure)
		{
			return Result.Failure<Review>(userIdResult.Error);
		}

		Result<MovieId> movieIdResult = MovieId.Create(movieId);

		if (movieIdResult.IsFailure)
		{
			return Result.Failure<Review>(movieIdResult.Error);
		}

		Result<ReviewScore>	scoreResult = ReviewScore.Create(score);

		if (scoreResult.IsFailure)
		{
			return Result.Failure<Review>(scoreResult.Error);
		}

		return Result.Success<Review>(new(userIdResult.Value, movieIdResult.Value, scoreResult.Value));
	}
}
