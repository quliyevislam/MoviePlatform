using MoviePlatform.Domain.Common;
using MediatR;

namespace MoviePlatform.Application.Common.Messaging;

public interface ICommand : IRequest<Result>;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>;
