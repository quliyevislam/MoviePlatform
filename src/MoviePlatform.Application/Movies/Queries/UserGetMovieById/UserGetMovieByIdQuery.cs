using MoviePlatform.Application.Common.Messaging;
using MoviePlatform.Application.Movies.Queries.Shared;

namespace MoviePlatform.Application.Movies.Queries.UserGetMovieById;

public record UserGetMovieByIdQuery(int MovieId, int UserId) : IQuery<MovieResponse>;
