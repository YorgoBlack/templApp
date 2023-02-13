namespace Templ.API;
using Infrastucture;

record struct Configuration(string ConnectionString) : IAppConfiguration;