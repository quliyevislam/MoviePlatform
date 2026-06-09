using MoviePlatform.Application.Common.Messaging;

namespace MoviePlatform.Application.Movies.Commands.UserSubmitReview;

public record UserSubmitReviewCommand(int MovieId, int UserId, double Score) : ICommand;
