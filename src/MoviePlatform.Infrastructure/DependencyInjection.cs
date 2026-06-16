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
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

namespace MoviePlatform.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
		services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
		services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddSingleton<IJwtProvider, JwtProvider>();
		services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();

		var jwtConfiguration = configuration.GetSection("JwtSettings");
		var jwtOptions = jwtConfiguration.Get<JwtOptions>()!;

		services.Configure<JwtOptions>(jwtConfiguration);

		services
			.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(
				options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = jwtOptions.Issuer,
						ValidAudience = jwtOptions.Audience,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret))
					};
				}
			);

		services.AddAuthorization();

		return services;
    }
}
