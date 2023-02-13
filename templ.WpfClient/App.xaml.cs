using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Windows;

namespace templ.WpfClient;
using Services;


/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public IServiceProvider? ServiceProvider { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection, builder.Build() );

        ServiceProvider = serviceCollection.BuildServiceProvider();

        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    static private void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IConfiguration>(configuration);

        services.AddScoped<IService, CustomerService>();

        services.AddTransient(typeof(MainWindow));
    }
}
