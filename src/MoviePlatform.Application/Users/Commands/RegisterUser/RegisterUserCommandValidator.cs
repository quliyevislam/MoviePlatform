using FluentValidation;
using MoviePlatform.Domain.Users;

namespace MoviePlatform.Application.Users.Commands.RegisterUser;

public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
	public RegisterUserCommandValidator()
	{
		RuleFor(command => command.Name)
			.NotNull()
			.WithErrorCode(UserErrors.Name.Required.Code)
            .WithMessage(UserErrors.Name.Required.Description);

		RuleFor(command => command.Name)
			.NotEmpty()
			.WithErrorCode(UserErrors.Name.Empty.Code)
            .WithMessage(UserErrors.Name.Empty.Description);

		RuleFor(command => command.Name)
			.MaximumLength(UserConstants.Name.MaxLength)
			.WithErrorCode(UserErrors.Name.TooLong.Code)
            .WithMessage(UserErrors.Name.TooLong.Description);

		RuleFor(command => command.Email)
			.NotNull()
			.WithErrorCode(UserErrors.Email.Required.Code)
            .WithMessage(UserErrors.Email.Required.Description);

		RuleFor(command => command.Email)
			.NotEmpty()
			.WithErrorCode(UserErrors.Email.Empty.Code)
            .WithMessage(UserErrors.Email.Empty.Description);

		RuleFor(command => command.Email)
			.MaximumLength(UserConstants.Email.MaxLength)
			.WithErrorCode(UserErrors.Email.TooLong.Code)
            .WithMessage(UserErrors.Email.TooLong.Description);

		RuleFor(command => command.Password)
			.NotNull()
			.WithErrorCode(UserErrors.Password.Required.Code)
            .WithMessage(UserErrors.Password.Required.Description);

		RuleFor(command => command.Password)
			.NotEmpty()
			.WithErrorCode(UserErrors.Password.Empty.Code)
            .WithMessage(UserErrors.Password.Empty.Description);

		RuleFor(command => command.Password)
			.MinimumLength(UserConstants.Password.MinLength)
			.WithErrorCode(UserErrors.Password.TooShort.Code)
            .WithMessage(UserErrors.Password.TooShort.Description);
	}
}
