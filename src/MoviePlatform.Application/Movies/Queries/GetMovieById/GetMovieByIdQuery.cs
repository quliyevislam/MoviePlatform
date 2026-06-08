using MoviePlatform.Application.Common.Messaging;
using MoviePlatform.Application.Movies.Queries.Shared;

namespace MoviePlatform.Application.Movies.Queries.GetMovieById;

public record GetMovieByIdQuery(int MovieId, int UserId) : IQuery<MovieResponse>;
