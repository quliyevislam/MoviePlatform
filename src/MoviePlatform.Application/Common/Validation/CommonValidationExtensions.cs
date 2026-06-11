using FluentValidation;
using MoviePlatform.Domain.Users;

namespace MoviePlatform.Application.Common.Validation;

public static class CommonValidationExtensions
{
	public static IRuleBuilderOptions<T, string> ValidPassword<T>(this IRuleBuilder<T, string> ruleBuilder) {
		return ruleBuilder
			.NotNull()
			.WithErrorCode(UserErrors.Password.Required.Code)
            .WithMessage(UserErrors.Password.Required.Description)

			.NotEmpty()
			.WithErrorCode(UserErrors.Password.Empty.Code)
            .WithMessage(UserErrors.Password.Empty.Description)

			.MinimumLength(UserConstants.Password.MinLength)
			.WithErrorCode(UserErrors.Password.TooShort.Code)
            .WithMessage(UserErrors.Password.TooShort.Description)

			.Matches(UserConstants.Password.RequireUppercase)
			.WithErrorCode(UserErrors.Password.UppercaseRequired.Code)
            .WithMessage(UserErrors.Password.UppercaseRequired.Description)

			.Matches(UserConstants.Password.RequireLowercase)
			.WithErrorCode(UserErrors.Password.LowercaseRequired.Code)
            .WithMessage(UserErrors.Password.LowercaseRequired.Description)

			.Matches(UserConstants.Password.RequireDigit)
			.WithErrorCode(UserErrors.Password.DigitRequired.Code)
            .WithMessage(UserErrors.Password.DigitRequired.Description)

			.Matches(UserConstants.Password.RequireNoSpace)
			.WithErrorCode(UserErrors.Password.SpacesNotAllowed.Code)
            .WithMessage(UserErrors.Password.SpacesNotAllowed.Description);
	}

	public static IRuleBuilderOptions<T, string> ValidEmail<T>(this IRuleBuilder<T, string> ruleBuilder)
	{
		return ruleBuilder
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
	}

	public static IRuleBuilderOptions<T, string> ValidName<T>(this IRuleBuilder<T, string> ruleBuilder)
	{
		return ruleBuilder
			.NotNull()
			.WithErrorCode(UserErrors.Name.Required.Code)
            .WithMessage(UserErrors.Name.Required.Description)

			.NotEmpty()
			.WithErrorCode(UserErrors.Name.Empty.Code)
            .WithMessage(UserErrors.Name.Empty.Description)

			.MaximumLength(UserConstants.Name.MaxLength)
			.WithErrorCode(UserErrors.Name.TooLong.Code)
            .WithMessage(UserErrors.Name.TooLong.Description);
	}
}
