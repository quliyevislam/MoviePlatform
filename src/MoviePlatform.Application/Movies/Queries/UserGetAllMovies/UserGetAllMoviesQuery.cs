using MoviePlatform.Application.Common.Messaging;
using MoviePlatform.Application.Movies.Queries.Shared;

namespace MoviePlatform.Application.Movies.Queries.UserGetAllMovies;

public record UserGetAllMoviesQuery(int UserId) : IQuery<List<MovieResponse>>;
