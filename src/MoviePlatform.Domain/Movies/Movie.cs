using MoviePlatform.Domain.Common;
using MoviePlatform.Domain.Movies.Enums;
using MoviePlatform.Domain.Movies.Entities;
using MoviePlatform.Domain.Movies.Events;
using MoviePlatform.Domain.Movies.ValueObjects;
using MoviePlatform.Domain.Users.ValueObjects;

namespace MoviePlatform.Domain.Movies;

public sealed class Movie : AggregateRoot<MovieId>
{
	public UserId UserId { get; private set; }
	public Title Title { get; private set; }
	public Description Description { get; private set; }
	public Genre Genre { get; private set; }
	public ReleaseDate ReleaseDate { get; private set; }
	public AverageRating AverageRating { get; private set; }

	private readonly List<Review> _reviews = [];
	private readonly List<Comment> _comments = [];

	public IReadOnlyCollection<Review> Reviews => _reviews.AsReadOnly();
	public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();

	private Movie()
	{
	}

	private Movie(UserId userId, Title title, Description description, Genre genre, ReleaseDate releaseDate)
	{
		UserId = userId;
		Title = title;
		Description = description;
		Genre = genre;
		ReleaseDate = releaseDate;
		AverageRating = new AverageRating();
	}

	public static Result<Movie> Create(int userId, string title, string description, Genre genre, DateOnly releaseDate, DateTime currentUtcTime)
	{
		Result<UserId> userIdResult = UserId.Create(userId);

		if (userIdResult.IsFailure)
		{
			return Result.Failure<Movie>(userIdResult.Error);
		}

		Result<Title> titleResult = Title.Create(title);

		if (titleResult.IsFailure)
		{
			return Result.Failure<Movie>(titleResult.Error);
		}

		Result<Description> descriptionResult = Description.Create(description);

		if (descriptionResult.IsFailure)
		{
			return Result.Failure<Movie>(descriptionResult.Error);
		}

		Result<ReleaseDate> releaseDateResult = ReleaseDate.Create(releaseDate, currentUtcTime);

		if (releaseDateResult.IsFailure)
		{
			return Result.Failure<Movie>(releaseDateResult.Error);
		}

		return Result.Success<Movie>(new(userIdResult.Value, titleResult.Value, descriptionResult.Value, genre, releaseDateResult.Value));
	}

	public Result SubmitReview(int userId, float score)
	{
		Review? existingReview = _reviews.FirstOrDefault(review => review.UserId.Value == userId);

		if (existingReview is null)
		{
			Result<Review> reviewResult = Review.Create(userId, score);

			if (reviewResult.IsFailure)
			{
				return Result.Failure(reviewResult.Error);
			}

			_reviews.Add(reviewResult.Value);
		}
		else
		{
			Result<ReviewScore> reviewScoreResult = ReviewScore.Create(score);

			if (reviewScoreResult.IsFailure)
			{
				return Result.Failure(reviewScoreResult.Error);
			}

			existingReview.UpdateScore(reviewScoreResult.Value);
		}

		RaiseDomainEvent(new ReviewSubmittedDomainEvent(Id));

		return Result.Success();
	}

	public Result AddComment(int userId, string content)
	{
		Result<Comment> commentResult = Comment.Create(userId, content);

		if (commentResult.IsFailure)
		{
			return Result.Failure(commentResult.Error);
		}

		_comments.Add(commentResult.Value);

		return Result.Success();
	}
}
