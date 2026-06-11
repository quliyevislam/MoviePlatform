using MediatR;
using FluentValidation;
using FluentValidation.Results;
using MoviePlatform.Application.Common.Authentication;
using MoviePlatform.Application.Common.Messaging;
using MoviePlatform.Application.Common.Data;
using MoviePlatform.Domain.Common;
using MoviePlatform.Domain.Users;
using MoviePlatform.Domain.Users.ValueObjects;

namespace MoviePlatform.Application.Users.Commands.RegisterUser;

internal sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<int>>
{
	private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;
	private readonly IValidator<RegisterUserCommand> _validator;

	public RegisterUserCommandHandler(
		IUserRepository userRepository,
		IPasswordHasher passwordHasher,
		IUnitOfWork unitOfWork,
		IValidator<RegisterUserCommand> validator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
		_validator = validator;
	}

	public async Task<Result<int>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
		ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);

		if (!validationResult.IsValid)
        {
            ValidationFailure firstFailure = validationResult.Errors.First();

            return Result.Failure<int>(Error.Validation(firstFailure.ErrorCode, firstFailure.ErrorMessage));
        }

		Result<Email> emailResult = Email.Create(request.Email);

		if (emailResult.IsFailure)
		{
			return Result.Failure<int>(emailResult.Error);
		}

		bool isEmailUnique = await _userRepository.IsEmailUniqueAsync(emailResult.Value, cancellationToken);

		if (!isEmailUnique)
        {
            return Result.Failure<int>(UserErrors.EmailAlreadyInUse);
        }

        string passwordHash = _passwordHasher.Hash(request.Password);

        Result<User> userResult = User.Create(request.Name, request.Email, passwordHash);

		if (userResult.IsFailure)
		{
			return Result.Failure<int>(userResult.Error);
		}

        _userRepository.Add(userResult.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(userResult.Value.Id.Value);
    }
}
