using MoviePlatform.Domain.Movies.Enums;

namespace MoviePlatform.Application.Movies.Queries.Shared;

public record MovieResponse(int MoiveId, string Title, string Description, string Genre, DateOnly ReleaseDate, double AverageRating, int ReviewCount);
