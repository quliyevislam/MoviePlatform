using MediatR;
using FluentValidation;
using FluentValidation.Results;
using MoviePlatform.Application.Common.Messaging;
using MoviePlatform.Application.Common.Data;
using MoviePlatform.Domain.Common;
using MoviePlatform.Domain.Movies;

namespace MoviePlatform.Application.Movies.Commands.UserCreateMovie;

public sealed class UserCreateMovieCommandHandler : IRequestHandler<UserCreateMovieCommand, Result<int>>
{
	private readonly IMovieRepository _movieRepository;
    private readonly IUnitOfWork _unitOfWork;
	private readonly IValidator<UserCreateMovieCommand> _validator;
	private readonly TimeProvider _timeProvider;

	public UserCreateMovieCommandHandler(
		IMovieRepository movieRepository,
		IUnitOfWork unitOfWork,
		IValidator<UserCreateMovieCommand> validator,
		TimeProvider timeProvider)
	{
		_movieRepository = movieRepository;
		_unitOfWork = unitOfWork;
		_validator = validator;
		_timeProvider = timeProvider;
	}

	public async Task<Result<int>> Handle(UserCreateMovieCommand request, CancellationToken cancellationToken)
	{
		ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);

		if (!validationResult.IsValid)
        {
            ValidationFailure firstFailure = validationResult.Errors.First();

            return Result.Failure<int>(Error.Validation(firstFailure.ErrorCode, firstFailure.ErrorMessage));
        }

		Result<Movie> movieResult = Movie.Create(
			request.UserId,
			request.Title,
			request.Description,
			request.Genre,
			request.ReleaseDate,
			_timeProvider.GetUtcNow());

		if (movieResult.IsFailure)
		{
			return Result.Failure<int>(movieResult.Error);
		}

		_movieRepository.Add(movieResult.Value);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return Result.Success(movieResult.Value.Id.Value);
	}
}
