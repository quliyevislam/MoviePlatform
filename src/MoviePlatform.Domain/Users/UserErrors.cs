using MoviePlatform.Domain.Common;

namespace MoviePlatform.Domain.Users;

public static class UserErrors
{
	public static class UserId
	{
		public static readonly Error Invalid = Error.Validation(
			"User.UserId.Invalid",
			"The user id is invalid."
		);
	}
}
