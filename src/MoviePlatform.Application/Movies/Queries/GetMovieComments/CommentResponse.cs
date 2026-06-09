namespace MoviePlatform.Application.Movies.Queries.GetMovieComments;

public record CommentResponse(int CommentId, int MovieId, string Author, string Content);
