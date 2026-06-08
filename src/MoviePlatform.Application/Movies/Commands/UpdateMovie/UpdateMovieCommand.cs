using MoviePlatform.Domain.Movies.Enums;
using MoviePlatform.Application.Common.Messaging;

namespace MoviePlatform.Application.Movies.Commands.UpdateMovie;

public record UpdateMovieCommand(int MovieId, int UserId, string Title, string Description, Genre Genre, DateOnly ReleaseDate) : ICommand;
