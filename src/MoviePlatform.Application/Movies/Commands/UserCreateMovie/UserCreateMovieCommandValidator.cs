using FluentValidation;
using MoviePlatform.Application.Common.Validation;

namespace MoviePlatform.Application.Movies.Commands.UserCreateMovie;

public sealed class UserCreateMovieCommandValidator : AbstractValidator<UserCreateMovieCommand>
{
	private readonly TimeProvider _timeProvider;

	public UserCreateMovieCommandValidator(TimeProvider timeProvider)
	{
		_timeProvider = timeProvider;
		ClassLevelCascadeMode = CascadeMode.Stop;

		RuleFor(command => command.UserId).ValidUserId();
		RuleFor(command => command.Title).ValidTitle();
		RuleFor(command => command.Description).ValidDescription();
		RuleFor(command => command.Genre).ValidGenre();
		RuleFor(command => command.ReleaseDate).ValidReleaseDate(_timeProvider);
	}
}
