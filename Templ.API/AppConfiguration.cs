namespace Templ.API;
using Infrastucture;

public record struct AppConfiguration(string ConnectionString, string LiteDbPath, string JwtSecretKey) : IAppConfiguration;
