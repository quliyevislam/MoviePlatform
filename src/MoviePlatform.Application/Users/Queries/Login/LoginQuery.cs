using MoviePlatform.Application.Common.Messaging;

namespace MoviePlatform.Application.Users.Queries.Login;

public record LoginQuery(string Email, string Password) : IQuery<LoginResponse>;
