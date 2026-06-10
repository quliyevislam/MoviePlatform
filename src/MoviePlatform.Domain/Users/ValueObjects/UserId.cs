using MoviePlatform.Domain.Common;
using MoviePlatform.Domain.Users;

namespace MoviePlatform.Domain.Users.ValueObjects;

public readonly record struct UserId
{
	public int Value { get; }

	private UserId(int value) => Value = value;

	public static Result<UserId> Create(int value)
	{
		// if (value <= 0)
		// {
		//		return Result.Failure<UserId>(UserErrors.UserId.Invalid);
		// }

		return Result.Success<UserId>(new(value));
	}
}
