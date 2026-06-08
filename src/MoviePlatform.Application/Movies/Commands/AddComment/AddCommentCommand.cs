using MoviePlatform.Application.Common.Messaging;

namespace MoviePlatform.Application.Movies.Commands.AddComment;

public record AddCommentCommand(int MovieId, int UserId, string Content) : ICommand<int>;
