using MoviePlatform.Domain.Common;
using MoviePlatform.Domain.Users.ValueObjects;

namespace MoviePlatform.Domain.Users;

public sealed class User : AggregateRoot<UserId>
{
	public Name Name { get; private set; }
	public Email Email { get; private set; }
	public PasswordHash PasswordHash { get; private set; }

	private User() { }

	private User(Name name, Email email, PasswordHash passwordHash)
	{
		Name = name;
		Email = email;
		PasswordHash = passwordHash;
	}

	public static Result<User> Create(string name, string email, string passwordHash)
	{
		Result<Name> nameResult = Name.Create(name);

		if (nameResult.IsFailure)
		{
			return Result.Failure<User>(nameResult.Error);
		}

		Result<Email> emailResult = Email.Create(email);

		if (emailResult.IsFailure)
		{
			return Result.Failure<User>(emailResult.Error);
		}

		Result<PasswordHash> passwordHashResult = PasswordHash.Create(passwordHash);

		if (passwordHashResult.IsFailure)
		{
			return Result.Failure<User>(passwordHashResult.Error);
		}

		return Result.Success<User>(new(nameResult.Value, emailResult.Value, passwordHashResult.Value));
	}
}
