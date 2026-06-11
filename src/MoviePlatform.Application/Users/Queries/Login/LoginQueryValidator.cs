using FluentValidation;
using MoviePlatform.Domain.Users;
using MoviePlatform.Application.Common.Validation;

namespace MoviePlatform.Application.Users.Queries.Login;

public sealed class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
		ClassLevelCascadeMode = CascadeMode.Stop;

		RuleFor(command => command.Email).ValidEmail();
		RuleFor(query => query.Password).ValidPassword();
	}
}
