using FluentValidation;
using MoviePlatform.Domain.Users;

namespace MoviePlatform.Application.Users.Queries.Login;

public sealed class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
		ClassLevelCascadeMode = CascadeMode.Stop;

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

		RuleFor(query => query.Password)
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
