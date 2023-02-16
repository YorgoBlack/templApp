namespace Templ.Infrastucture.Services;

public interface IAuthService
{
    string Token(string userId);
    string? ValidateToken(string token);
}
