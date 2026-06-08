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
	public ReviewScore AverageRating { get; private set; }

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
		AverageRating = new ReviewScore();
	}

	public static Movie Create(UserId userId, Title title, Description description, Genre genre, ReleaseDate releaseDate)
	{
		return new(userId, title, description, genre, releaseDate);
	}

	public void Update(Title newTitle, Description newDescription, Genre newGenre, ReleaseDate newReleaseDate)
	{
		Title = newTitle;
		Description = newDescription;
		Genre = newGenre;
		ReleaseDate = newReleaseDate;
	}

	public void SubmitReview(UserId userId, ReviewScore score)
	{
		Review? review = _reviews.FirstOrDefault(review => review.UserId == userId);

		if (review is null)
		{
			Review newReview = Review.Create(userId, score);

			_reviews.Add(newReview);
		}
		else
		{
			review.UpdateScore(score);
		}

		RaiseDomainEvent(new ReviewSubmittedDomainEvent(Id));
	}

	public void AddComment(UserId userId, CommentContent content)
	{
		Comment newComment = Comment.Create(userId, content);

		_comments.Add(newComment);
	}

	public Result DeleteComment(UserId userId, CommentId commentId)
	{
		Comment? comment = _comments.FirstOrDefault(comment => comment.Id == commentId);

		if (comment is null)
		{
			return Result.Failure(MovieErrors.Comment.NotFound);
		}

		if (comment.UserId != userId)
		{
			return Result.Failure(MovieErrors.Comment.Forbidden);
		}

		_comments.Remove(comment);

		return Result.Success();
	}
}
