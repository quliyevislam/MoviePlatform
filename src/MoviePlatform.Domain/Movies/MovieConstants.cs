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
		public static readonly int MaxDigitsPrecision = 3;
		public static readonly int DecimalPlacesScale = 2;

		public static readonly double MinScore = 1.00D;
		public static readonly double MaxScore = 5.00D;
	}

	public static class CommentContent
	{
		public static readonly int MaxLength = 500;
	}
}
