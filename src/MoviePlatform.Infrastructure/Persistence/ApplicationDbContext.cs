using Microsoft.EntityFrameworkCore;
using MoviePlatform.Domain.Users;
using MoviePlatform.Domain.Movies;

namespace MoviePlatform.Infrastructure.Persistence;

public sealed class ApplicationDbContext : DbContext
{
	public DbSet<User> Users => Set<User>();
	public DbSet<Movie> Movies => Set<Movie>();

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
	}
}
