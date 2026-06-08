using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoviePlatform.Application.Common.Authentication;
using MoviePlatform.Application.Common.Data;
using MoviePlatform.Infrastructure.Authentication;
using MoviePlatform.Infrastructure.Persistence;

namespace MoviePlatform.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Database")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));

        services.AddSingleton<IJwtProvider, JwtProvider>();

        return services;
    }
}
