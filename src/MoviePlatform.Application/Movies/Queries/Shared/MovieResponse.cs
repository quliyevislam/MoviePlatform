using MoviePlatform.Domain.Movies.Enums;

namespace MoviePlatform.Application.Movies.Queries.Shared;

public record MovieResponse(string MovieId, string Title, string Description, Genre Genre, DateOnly ReleaseDate);
