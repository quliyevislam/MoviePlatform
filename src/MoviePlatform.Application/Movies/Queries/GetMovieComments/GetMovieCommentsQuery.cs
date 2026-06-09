using MoviePlatform.Application.Common.Messaging;

namespace MoviePlatform.Application.Movies.Queries.GetMovieComments;

public record GetMovieCommentsQuery(int MovieId) : IQuery<CommentResponse>;
