using MoviePlatform.Domain.Common;
using MoviePlatform.Domain.Movies.ValueObjects;

namespace MoviePlatform.Domain.Movies.Events;

public sealed record ReviewSubmittedDomainEvent(MovieId MovieId) : DomainEvent;
