using MoviePlatform.Domain.Common;
using MoviePlatform.Domain.Users.ValueObjects;

namespace MoviePlatform.Domain.Users;

public sealed class User : AggregateRoot<UserId>
{
	public Name Name { get; private set; }
	public Email Email { get; private set; }
	public PasswordHash PasswordHash { get; private set; }

	private User()
	{
	}

	private User(Name name, Email email, PasswordHash passwordHash)
	{
		Name = name;
		Email = email;
		PasswordHash = passwordHash;
	}

	public static User Create(Name name, Email email, PasswordHash passwordHash)
	{
		return new(name, email, passwordHash);
	}
}
