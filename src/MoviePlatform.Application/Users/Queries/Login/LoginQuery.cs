using System.Text.Json.Serialization;
using MoviePlatform.Application.Common.Messaging;

namespace MoviePlatform.Application.Users.Queries.Login;

public record LoginQuery : IQuery<LoginResponse>
{
	public string Email { get; init; }
	public string Password { get; init; }

    [JsonConstructor]
	public LoginQuery(string email, string password)
	{
		Email = email.Trim();
		Password = password.Trim();
	}
}
