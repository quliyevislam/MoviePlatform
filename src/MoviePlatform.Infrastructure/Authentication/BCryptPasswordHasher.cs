using MoviePlatform.Application.Common.Authentication;
using BCryptNet = BCrypt.Net.BCrypt;

namespace MoviePlatform.Infrastructure.Authentication;

internal sealed class BCryptPasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        return BCryptNet.HashPassword(password);
    }

    public bool Verify(string password, string passwordHash)
    {
        return BCryptNet.Verify(password, passwordHash);
    }
}
