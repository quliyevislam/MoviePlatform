using MoviePlatform.Domain.Common;

namespace MoviePlatform.Domain.Movies;

public static class MovieErrors
{
	public static class MovieId
	{
		public static readonly Error Invalid = Error.Validation(
			"Movie.MovieId.Invalid",
			"The provided movie id is invalid."
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
}
