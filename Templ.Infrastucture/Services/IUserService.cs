namespace Templ.Infrastucture.Services;
using Domain;

public interface IUserService
{
    AppUser ValidatePassword(string username, string password);
    AppUser Create(string username, string password, string firstName, string? lastName);
    AppUser FindByUserName(string username);
}
