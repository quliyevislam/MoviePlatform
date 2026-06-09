namespace MoviePlatform.Application.Movies.Queries.Shared;

public record CommentResponse(int CommentId, int UserId, string Content);
