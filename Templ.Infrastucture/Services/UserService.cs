using FluentMediator.Pipelines;
using LiteDB;
using System.Security.Cryptography.X509Certificates;
using Templ.Domain;

namespace Templ.Infrastucture.Services;

class Secret : HasUserName
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class UserService : IUserService, IDisposable
{
    private readonly IAppConfiguration _configuration;
    private readonly LiteDatabase _db;

    public UserService(IAppConfiguration configuration)
    {
        _configuration = configuration;
        EnsureDb();
        _db = new LiteDatabase(_configuration.LiteDbPath);
    }

    public AppUser FindByUserName(string username)
    {
        return Query<AppUser>(c => c.FindOne(u=>u.UserName.Equals(username)));
    }

    public AppUser ValidatePassword(string username, string password)
    {
        return Query<AppUser>((users) =>
        {
            var secret = Query<Secret>(s => s.FindOne(x => x.UserName.Equals(username) && x.Password.Equals(password)));
            if( secret != null)
            {
                return users.FindOne(x => x.UserName.Equals(username));
            }
            return null!;
        });
    }

    public AppUser Create(string username, string password, string firstName, string? lastName)
    {
        if (FindByUserName(username) == null)
        {
            var id = Query<Secret>(s => s.Insert(new Secret { UserName = username, Password = password }));
            if (id != null)
            {
                return Query<AppUser>(u =>
                {
                    u.Insert(new AppUser
                    {
                        UserName = username,
                        FirstName = firstName,
                        LastName = lastName
                    });
                    return u.FindOne(x => x.UserName.Equals(username));
                });
            }
        }
        return null!;
    }

    public void Dispose()
    {
        _db?.Dispose();
    }

    private T Query<T>(Func<ILiteCollection<T>, T> command) where T : HasUserName 
    {
        var collection = _db.GetCollection<T>();
        collection.EnsureIndex(x=>x.UserName, true);
        return command(collection);
    }

    private BsonValue Query<T>(Func<ILiteCollection<T>, BsonValue> command) where T : HasUserName
    {
        var collection = _db.GetCollection<T>();
        collection.EnsureIndex(x => x.UserName, true);
        return command(collection);
    }

    private void EnsureDb()
    {
        var path = Path.GetDirectoryName(_configuration.LiteDbPath);
        if (path == null) throw new ArgumentNullException(path);
        Directory.CreateDirectory(path);
    }
}