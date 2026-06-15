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
	public ReviewCount ReviewCount { get; private set; }

	private readonly List<Review> _reviews = [];
	private readonly List<Comment> _comments = [];

	public IReadOnlyCollection<Review> Reviews => _reviews.AsReadOnly();
	public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();

	private Movie() { }

	private Movie(UserId userId, Title title, Description description, Genre genre, ReleaseDate releaseDate)
	{
		UserId = userId;
		Title = title;
		Description = description;
		Genre = genre;
		ReleaseDate = releaseDate;
		AverageRating = AverageRating.Create(0).Value;
		ReviewCount = ReviewCount.Create(0).Value;
	}

	public static Result<Movie> Create(int userId, string title, string description, string genre, DateOnly releaseDate, DateTimeOffset currentUtcTime)
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

		Result<Genre> genreResult = Genre.Create(genre);

		if (genreResult.IsFailure)
		{
			return Result.Failure<Movie>(genreResult.Error);
		}

		Result<ReleaseDate> releaseDateResult = ReleaseDate.Create(releaseDate, currentUtcTime);

		if (releaseDateResult.IsFailure)
		{
			return Result.Failure<Movie>(releaseDateResult.Error);
		}

		return Result.Success<Movie>(new(userIdResult.Value, titleResult.Value, descriptionResult.Value, genreResult.Value, releaseDateResult.Value));
	}

	public Result Update(string newTitle, string newDescription, Genre newGenre, DateOnly newReleaseDate, DateTime currentUtcTime)
	{
		Result<Title> newTitleResult = Title.Create(newTitle);

		if (newTitleResult.IsFailure)
		{
			return Result.Failure(newTitleResult.Error);
		}

		Result<Description> newDescriptionResult = Description.Create(newDescription);

		if (newDescriptionResult.IsFailure)
		{
			return Result.Failure(newDescriptionResult.Error);
		}

		Result<ReleaseDate> newReleaseDateResult = ReleaseDate.Create(newReleaseDate, currentUtcTime);

		if (newReleaseDateResult.IsFailure)
		{
			return Result.Failure(newReleaseDateResult.Error);
		}

		Title = newTitleResult.Value;
		Description = newDescriptionResult.Value;
		Genre = newGenre;
		ReleaseDate = newReleaseDateResult.Value;

		return Result.Success();
	}

	private void RecalculateAverageRating()
	{
		int totalReviewCount = _reviews.Count;

		// ReviewCount Result never fails
		ReviewCount = ReviewCount.Create(totalReviewCount).Value;

		if (totalReviewCount == 0)
		{
			AverageRating = AverageRating.Create(0).Value;
			return;
		}

		double totalScoreSum = _reviews.Sum(review => review.Score.Value);
		AverageRating =	AverageRating.Create(totalScoreSum / totalReviewCount).Value;
	}

	public Result SubmitReview(Review newReview)
	{
		Review? existingReview = _reviews.FirstOrDefault(
			existingReview => existingReview.UserId == newReview.UserId
		);

		if (existingReview is null)
		{
			_reviews.Add(newReview);
		}
		else
		{
			existingReview.UpdateScore(newReview.Score);
		}

		RecalculateAverageRating();

		return Result.Success();
	}

	public Result AddComment(Comment newComment)
	{
		_comments.Add(newComment);

		return Result.Success();
	}

	public Result DeleteComment(Comment comment)
	{
		Comment? existingComment = _comments.FirstOrDefault(
			existingComment => existingComment.Id == comment.Id
		);

		if (existingComment is null)
		{
			return Result.Failure(MovieErrors.Comment.NotFound);
		}

		if (existingComment.UserId != comment.UserId)
		{
			return Result.Failure(MovieErrors.Comment.Forbidden);
		}

		_comments.Remove(comment);

		return Result.Success();
	}
}
