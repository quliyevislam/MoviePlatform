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

		public static readonly float MinScore = 1.00F;
		public static readonly float MaxScore = 5.00F;
	}

	public static class CommentContent
	{
		public static readonly int MaxLength = 500;
	}
}
