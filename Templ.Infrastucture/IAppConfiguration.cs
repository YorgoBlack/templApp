namespace Templ.Infrastucture;

public interface IAppConfiguration
{
    string ConnectionString { get; }
    string LiteDbPath { get; }
    string JwtSecretKey { get; }
}
