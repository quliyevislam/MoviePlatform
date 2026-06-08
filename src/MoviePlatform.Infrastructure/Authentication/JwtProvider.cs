using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MoviePlatform.Domain.Users;
using MoviePlatform.Application.Common.Authentication;

namespace MoviePlatform.Infrastructure.Authentication;

public sealed class JwtProvider : IJwtProvider
{
	private readonly JwtOptions _jwtOptions;

	public JwtProvider(IOptions<JwtOptions> jwtOptions)
	{
		_jwtOptions = jwtOptions.Value;
	}

	public string Generate(User user)
	{
		Claim[] claims = new Claim[]
		{
			new(JwtRegisteredClaimNames.Sub, user.Id.Value.ToString()),
			new(JwtRegisteredClaimNames.Email, user.Email.Value)
		};

		SigningCredentials signingCredentials = new SigningCredentials(
			new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
            SecurityAlgorithms.HmacSha256
		);

		JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
			_jwtOptions.Issuer,
			_jwtOptions.Audience,
			claims,
			null,
			DateTime.UtcNow.AddHours(1),
			signingCredentials
		);

		string tokenValue = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

		return tokenValue;
	}
}
