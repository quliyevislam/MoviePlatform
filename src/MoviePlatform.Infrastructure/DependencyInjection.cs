using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoviePlatform.Domain.Movies;
using MoviePlatform.Domain.Users;
using MoviePlatform.Application.Common.Authentication;
using MoviePlatform.Application.Common.Data;
using MoviePlatform.Infrastructure.Authentication;
using MoviePlatform.Infrastructure.Persistence;
using MoviePlatform.Infrastructure.Persistence.Repositories;

namespace MoviePlatform.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Database")));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
		services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
		services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
        services.AddSingleton<IJwtProvider, JwtProvider>();

        return services;
    }
}
