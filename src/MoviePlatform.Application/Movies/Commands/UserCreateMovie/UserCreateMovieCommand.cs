using MoviePlatform.Domain.Movies.Enums;
using MoviePlatform.Application.Common.Messaging;

namespace MoviePlatform.Application.Movies.Commands.UserCreateMovie;

public record UserCreateMovieCommand(int UserId, string Title, string Description, Genre Genre, DateOnly ReleaseDate) : ICommand<int>;
