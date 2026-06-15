using FluentValidation;
using MoviePlatform.Domain.Users;
using MoviePlatform.Domain.Movies;
using MoviePlatform.Domain.Movies.Enums;

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

	public static IRuleBuilderOptions<T, int> ValidUserId<T>(this IRuleBuilder<T, int> ruleBuilder)
	{
		return ruleBuilder
			.GreaterThan(0)
			.WithErrorCode(UserErrors.UserId.Invalid.Code)
            .WithMessage(UserErrors.UserId.Invalid.Description);
	}


	public static IRuleBuilderOptions<T, string> ValidTitle<T>(this IRuleBuilder<T, string> ruleBuilder)
	{
		return ruleBuilder
			.NotNull()
			.WithErrorCode(MovieErrors.Title.Required.Code)
            .WithMessage(MovieErrors.Title.Required.Description)

			.NotEmpty()
			.WithErrorCode(MovieErrors.Title.Empty.Code)
            .WithMessage(MovieErrors.Title.Empty.Description)

			.MaximumLength(MovieConstants.Title.MaxLength)
			.WithErrorCode(MovieErrors.Title.TooLong.Code)
            .WithMessage(MovieErrors.Title.TooLong.Description);
	}

	public static IRuleBuilderOptions<T, string> ValidDescription<T>(this IRuleBuilder<T, string> ruleBuilder)
	{
		return ruleBuilder
			.MaximumLength(MovieConstants.Description.MaxLength)
			.WithErrorCode(MovieErrors.Description.TooLong.Code)
            .WithMessage(MovieErrors.Description.TooLong.Description)
			.When(description => description is not null);
	}

	public static IRuleBuilderOptions<T, Genre> ValidGenre<T>(this IRuleBuilder<T, Genre> ruleBuilder)
	{
		return ruleBuilder
			.IsInEnum()
			.WithErrorCode(MovieErrors.Genre.Invalid.Code)
            .WithMessage(MovieErrors.Genre.Invalid.Description);
	}

	public static IRuleBuilderOptions<T, DateOnly> ValidReleaseDate<T>(this IRuleBuilder<T, DateOnly> ruleBuilder, DateTimeOffset currentUtcTime)
	{
		return ruleBuilder
			.LessThanOrEqualTo(DateOnly.FromDateTime(currentUtcTime.DateTime))
			.WithErrorCode(MovieErrors.ReleaseDate.InFuture.Code)
            .WithMessage(MovieErrors.ReleaseDate.InFuture.Description);
	}
}
