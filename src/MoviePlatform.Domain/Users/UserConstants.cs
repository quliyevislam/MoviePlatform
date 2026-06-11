namespace MoviePlatform.Domain.Users;

public static class UserConstants
{
	public static class Name
	{
		public static readonly int MaxLength = 50;
	}

	public static class Email
	{
		public static readonly int MaxLength = 254;
	}

	public static class Password
	{
		public static readonly string Pattern = @"^(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?!.*\s).*$";
	}
}
