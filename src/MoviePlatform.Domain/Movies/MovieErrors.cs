using MoviePlatform.Domain.Common;

namespace MoviePlatform.Domain.Movies;

public static class MovieErrors
{
	public static class MovieId
	{
		public static readonly Error Invalid = Error.Validation(
			"Movie.MovieId.Invalid",
			"The movie id is invalid."
		);
	}

	public static class Title
	{
		public static readonly Error Required = Error.Validation(
			"Movie.Title.Required",
			"The movie title is required."
		);

		public static readonly Error Empty = Error.Validation(
			"Movie.Title.Empty",
			"The movie title cannot be empty."
		);

		public static readonly Error TooLong = Error.Validation(
			"Movie.Title.TooLong",
			$"The movie title cannot exceed {MovieConstants.Title.MaxLength} characters."
		);
	}

	public static class Description
	{
		public static readonly Error TooLong = Error.Validation(
			"Movie.Description.TooLong",
			$"The movie description cannot exceed {MovieConstants.Description.MaxLength} characters."
		);
	}

	public static class ReleaseDate
	{
		public static readonly Error InFuture = Error.Validation(
			"Movie.ReleaseDate.InFuture",
			"The movie release date cannot be in the future."
		);
	}

	public static class AverageRating
	{
		public static readonly Error OutOfRange = Error.Validation(
			"Movie.AverageRating.OutOfRange",
			$"The average rating must be between {MovieConstants.ReviewScore.MinScore} and {MovieConstants.ReviewScore.MaxScore}."
		);
	}

	public static class ReviewId
	{
		public static readonly Error Invalid = Error.Validation(
			"Movie.ReviewId.Invalid",
			"The review id is invalid."
		);
	}

	public static class ReviewScore
	{
		public static readonly Error OutOfRange = Error.Validation(
			"Movie.ReviewScore.OutOfRange",
			$"The review score must be between {MovieConstants.ReviewScore.MinScore} and {MovieConstants.ReviewScore.MaxScore}."
		);
	}

	public static class Comment
	{
		public static readonly Error NotFound = Error.NotFound(
			"Movie.Comment.NotFound",
			"The comment with the specified id was not found."
		);

		public static readonly Error Forbidden = Error.Forbidden(
			"Movie.Comment.Forbidden",
			"You do not have permission to delete this comment."
		);
	}

	public static class CommentId
	{
		public static readonly Error Invalid = Error.Validation(
			"Movie.CommentId.Invalid",
			"The comment id is invalid."
		);
	}

	public static class CommentContent
	{
		public static readonly Error Required = Error.Validation(
			"Movie.CommentContent.Required",
			"The comment content is rquired."
		);

		public static readonly Error Empty = Error.Validation(
			"Movie.CommentContent.Empty",
			"The comment content cannot be empty."
		);

		public static readonly Error TooLong = Error.Validation(
			"Movie.CommentContent.TooLong",
			$"The comment content cannot exceed {MovieConstants.CommentContent.MaxLength} characters."
		);
	}
}
