namespace Templ.Infrastucture.Services;

public interface IAuthService
{
    string Token(string userId);
    string? TryGetTokenUser(string token);
    (string?,string?) RefreshToken(string token);
}
