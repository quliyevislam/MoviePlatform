using MoviePlatform.Domain.Movies.Enums;
using MoviePlatform.Application.Common.Messaging;

namespace MoviePlatform.Application.Movies.Commands.UserUpdateMovie;

public record UserUpdateMovieCommand(int UserId, string NewTitle, string NewDescription, string NewGenre, DateOnly NewReleaseDate) : ICommand;
