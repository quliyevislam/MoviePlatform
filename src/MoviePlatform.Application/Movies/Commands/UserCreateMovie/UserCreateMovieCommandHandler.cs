using MediatR;
using MoviePlatform.Application.Common.Messaging;
using MoviePlatform.Application.Common.Data;
using MoviePlatform.Domain.Common;
using MoviePlatform.Domain.Movies;

namespace MoviePlatform.Application.Movies.Commands.UserCreateMovie;

public sealed class UserCreateMovieCommandHandler : IRequestHandler<UserCreateMovieCommand, Result<int>>
{
	private readonly IMovieRepository _movieRepository;
    private readonly IUnitOfWork _unitOfWork;
	private readonly TimeProvider _timeProvider;

	public UserCreateMovieCommandHandler(
		IMovieRepository movieRepository,
		IUnitOfWork unitOfWork,
		TimeProvider timeProvider)
	{
		_movieRepository = movieRepository;
		_unitOfWork = unitOfWork;
		_timeProvider = timeProvider;
	}

	public async Task<Result<int>> Handle(UserCreateMovieCommand request, CancellationToken cancellationToken)
	{
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
