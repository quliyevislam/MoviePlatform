using Microsoft.EntityFrameworkCore;
using MoviePlatform.Domain.Movies;
using MoviePlatform.Domain.Movies.ValueObjects;

namespace MoviePlatform.Infrastructure.Persistence.Repositories;

public sealed class MovieRepository : IMovieRepository
{
	private readonly ApplicationDbContext _context;

	public MovieRepository(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<Movie?> GetByIdAsync(MovieId movieId, CancellationToken cancellationToken = default)
    {
        return await _context.Movies.FirstOrDefaultAsync(movie => movie.Id == movieId, cancellationToken);
    }

	public async Task<Movie?> GetByIdWithCommentsAsync(MovieId movieId, CancellationToken cancellationToken = default)
    {
        return await _context.Movies
            .Include(movie => movie.Comments)
            .FirstOrDefaultAsync(movie => movie.Id == movieId, cancellationToken);
    }

	public void Add(Movie movie) => _context.Movies.Add(movie);
    public void Update(Movie movie) => _context.Movies.Update(movie);
}
