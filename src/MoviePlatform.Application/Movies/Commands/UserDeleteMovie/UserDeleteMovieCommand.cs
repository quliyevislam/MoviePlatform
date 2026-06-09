using MoviePlatform.Application.Common.Messaging;

namespace MoviePlatform.Application.Movies.Commands.UserDeleteMovie;

public record UserDeleteMovieCommand(int MovieId, int UserId) : ICommand;
