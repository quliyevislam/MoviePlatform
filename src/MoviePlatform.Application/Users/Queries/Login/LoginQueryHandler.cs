using MediatR;
using FluentValidation;
using FluentValidation.Results;
using MoviePlatform.Application.Common.Authentication;
using MoviePlatform.Application.Common.Messaging;
using MoviePlatform.Domain.Common;
using MoviePlatform.Domain.Users;
using MoviePlatform.Domain.Users.ValueObjects;

namespace MoviePlatform.Application.Users.Queries.Login;

internal sealed class LoginQueryHandler : IRequestHandler<LoginQuery, Result<LoginResponse>>
{
	private readonly IUserRepository _userRepository;
	private readonly IPasswordHasher _passwordHasher;
	private readonly IJwtProvider _jwtProvider;
	private readonly IValidator<LoginQuery> _validator;

	public LoginQueryHandler(
		IUserRepository userRepository,
		IPasswordHasher passwordHasher,
		IJwtProvider jwtProvider,
		IValidator<LoginQuery> validator)
	{
		_userRepository = userRepository;
		_passwordHasher = passwordHasher;
		_jwtProvider = jwtProvider;
		_validator = validator;
	}

	public async Task<Result<LoginResponse>> Handle(LoginQuery request, CancellationToken cancellationToken)
	{
		ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);

		if (!validationResult.IsValid)
		{
			ValidationFailure firstFailure = validationResult.Errors.First();

			return Result.Failure<LoginResponse>(
				Error.Validation(
					firstFailure.ErrorCode,
					firstFailure.ErrorMessage
				)
			);
		}

		Result<Email> emailResult = Email.Create(request.Email);

		if (emailResult.IsFailure)
		{
			return Result.Failure<LoginResponse>(emailResult.Error);
		}

		User? user = await _userRepository.GetByEmailAsync(emailResult.Value, cancellationToken);

		if (user is null)
		{
			return Result.Failure<LoginResponse>(UserErrors.InvalidCredentials);
		}

		bool isPasswordValid = _passwordHasher.Verify(request.Password, user.PasswordHash.Value);

		if (!isPasswordValid)
		{
			return Result.Failure<LoginResponse>(UserErrors.InvalidCredentials);
		}

		string token = _jwtProvider.Generate(user);

		return Result.Success(new LoginResponse(token));
	}
}
