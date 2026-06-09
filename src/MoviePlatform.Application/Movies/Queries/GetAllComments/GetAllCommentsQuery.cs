using MoviePlatform.Application.Common.Messaging;
using MoviePlatform.Application.Movies.Queries.Shared;

namespace MoviePlatform.Application.Movies.Queries.GetAllComments;

public record GetAllCommentsQuery() : IQuery<CommentResponse>;
