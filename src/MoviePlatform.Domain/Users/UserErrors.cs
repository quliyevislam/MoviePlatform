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

	public static class Name
	{
		public static readonly Error Required = Error.Validation(
			"User.Name.Required",
			"The user name is required."
		);

		public static readonly Error Empty = Error.Validation(
			"User.Name.Empty",
			"The user name cannot be empty."
		);

		public static readonly Error TooLong = Error.Validation(
			"User.Name.TooLong",
			$"The user name cannot exceed {UserConstants.Name.MaxLength} characters."
		);
	}

	public static class Email
	{
		public static readonly Error Required = Error.Validation(
			"User.Email.Required",
			"The user email is required."
		);

		public static readonly Error Empty = Error.Validation(
			"User.Email.Empty",
			"The user email cannot be empty."
		);

		public static readonly Error InvalidFormat = Error.Validation(
			"User.Email.InvalidFormat",
			"The user email format is invalid."
		);

		public static readonly Error TooLong = Error.Validation(
			"User.Email.TooLong",
			$"The user email cannot exceed {UserConstants.Email.MaxLength} characters."
		);
	}

	public static class PasswordHash
	{
		public static readonly Error Required = Error.Validation(
			"User.PasswordHash.Required",
			"The password hash is required."
		);

		public static readonly Error Empty = Error.Validation(
			"User.PasswordHash.Empty",
			"The password hash cannot be empty."
		);
	}
}
