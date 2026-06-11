using FluentValidation;
using MoviePlatform.Domain.Users;
using MoviePlatform.Application.Common.Validation;

namespace MoviePlatform.Application.Users.Commands.RegisterUser;

public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
	public RegisterUserCommandValidator()
	{
		ClassLevelCascadeMode = CascadeMode.Stop;

		RuleFor(command => command.Name).ValidName();
		RuleFor(command => command.Email).ValidEmail();
		RuleFor(command => command.Password).ValidPassword();
	}
}
