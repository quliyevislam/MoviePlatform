using FluentValidation;
using MoviePlatform.Domain.Users;

namespace MoviePlatform.Application.Users.Commands.RegisterUser;

public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
	public RegisterUserCommandValidator()
	{
		ClassLevelCascadeMode = CascadeMode.Stop;

		RuleFor(command => command.Name)
			.NotNull()
			.WithErrorCode(UserErrors.Name.Required.Code)
            .WithMessage(UserErrors.Name.Required.Description)

			.NotEmpty()
			.WithErrorCode(UserErrors.Name.Empty.Code)
            .WithMessage(UserErrors.Name.Empty.Description)

			.MaximumLength(UserConstants.Name.MaxLength)
			.WithErrorCode(UserErrors.Name.TooLong.Code)
            .WithMessage(UserErrors.Name.TooLong.Description);

		RuleFor(command => command.Email)
			.NotNull()
			.WithErrorCode(UserErrors.Email.Required.Code)
            .WithMessage(UserErrors.Email.Required.Description)

			.NotEmpty()
			.WithErrorCode(UserErrors.Email.Empty.Code)
            .WithMessage(UserErrors.Email.Empty.Description)

			.MaximumLength(UserConstants.Email.MaxLength)
			.WithErrorCode(UserErrors.Email.TooLong.Code)
            .WithMessage(UserErrors.Email.TooLong.Description)

			.EmailAddress()
			.WithErrorCode(UserErrors.Email.InvalidFormat.Code)
            .WithMessage(UserErrors.Email.InvalidFormat.Description);

		RuleFor(command => command.Password)
			.NotNull()
			.WithErrorCode(UserErrors.Password.Required.Code)
            .WithMessage(UserErrors.Password.Required.Description)

			.NotEmpty()
			.WithErrorCode(UserErrors.Password.Empty.Code)
            .WithMessage(UserErrors.Password.Empty.Description)

			.Matches(UserConstants.Password.Pattern)
			.WithErrorCode(UserErrors.Password.Weak.Code)
            .WithMessage(UserErrors.Password.Weak.Description);
	}
}
