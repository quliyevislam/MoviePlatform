namespace MoviePlatform.Domain.Movies;

public static class MovieConstants
{
	public static class Title
	{
		public static readonly int MaxLength = 255;
	}

	public static class Description
	{
		public static readonly int MaxLength = 500;
	}

	public static class ReviewScore
	{
		public static readonly float MinScore = 1.0F;
		public static readonly float MaxScore = 5.0F;
	}

	public static class CommentContent
	{
		public static readonly int MaxLength = 500;
	}
}
