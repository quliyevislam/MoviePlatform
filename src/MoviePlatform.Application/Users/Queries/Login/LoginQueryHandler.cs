using MediatR;
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

	public LoginQueryHandler(
		IUserRepository userRepository,
		IPasswordHasher passwordHasher,
		IJwtProvider jwtProvider)
	{
		_userRepository = userRepository;
		_passwordHasher = passwordHasher;
		_jwtProvider = jwtProvider;
	}

	public async Task<Result<LoginResponse>> Handle(LoginQuery request, CancellationToken cancellationToken)
	{
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

		Result<Password> passwordResult = Password.Create(request.Password);

		if (passwordResult.IsFailure)
		{
			return Result.Failure<LoginResponse>(passwordResult.Error);
		}

		bool isPasswordValid = _passwordHasher.Verify(passwordResult.Value, user.PasswordHash);

		if (!isPasswordValid)
		{
			return Result.Failure<LoginResponse>(UserErrors.InvalidCredentials);
		}

		string token = _jwtProvider.Generate(user);

		return Result.Success(new LoginResponse(token));
	}
}
