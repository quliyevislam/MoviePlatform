using MoviePlatform.Application.Common.Messaging;

namespace MoviePlatform.Application.Movies.Commands.UserDeleteComment;

public record UserDeleteCommentCommand(int MovieId, int UserId, int CommentId) : ICommand;
