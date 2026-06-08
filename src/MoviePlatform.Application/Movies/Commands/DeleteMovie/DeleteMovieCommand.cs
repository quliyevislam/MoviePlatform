using MoviePlatform.Application.Common.Messaging;

namespace MoviePlatform.Application.Movies.Commands.DeleteMovie;

public record DeleteMovieCommand(int MovieId, int UserId) : ICommand;
