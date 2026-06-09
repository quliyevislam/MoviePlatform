using MoviePlatform.Application.Common.Messaging;

namespace MoviePlatform.Application.Users.Commands.CreateUser;

public record CreateUserCommand(string Name, string Email, string Password) : ICommand<int>;
