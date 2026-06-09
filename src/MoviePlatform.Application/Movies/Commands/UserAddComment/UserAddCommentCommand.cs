using MoviePlatform.Application.Common.Messaging;

namespace MoviePlatform.Application.Movies.Commands.UserAddComment;

public record UserAddCommentCommand(int MovieId, int UserId, string Content) : ICommand<int>;
