using System.Text.Json.Serialization;
using MoviePlatform.Application.Common.Messaging;

namespace MoviePlatform.Application.Users.Commands.RegisterUser;

public record RegisterUserCommand : ICommand<int>
{
	public string Name { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }

    [JsonConstructor]
    public RegisterUserCommand(string name, string email, string password)
    {
        Name = name.Trim();
        Email = email.Trim();
        Password = password.Trim();
    }
}
