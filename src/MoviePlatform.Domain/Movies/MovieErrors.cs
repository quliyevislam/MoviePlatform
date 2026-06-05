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

	public static class Description
	{
		public static readonly Error TooLong = Error.Validation(
			"Movie.Description.TooLong",
			$"The movie description cannot exceed {MovieConstants.Description.MaxLength} characters."
		);
	}
}
