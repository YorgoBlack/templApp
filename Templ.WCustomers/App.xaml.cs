using System;
using System.IO;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Templ.WCustomers;
using Shared;
using ViewModels;
using Services;

public partial class App : Application
{
    public IServiceProvider? ServiceProvider { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        var configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
             .Build();

        var serviceCollection = new ServiceCollection();
    
        
        ConfigureServices(serviceCollection, configuration);

        ServiceProvider = serviceCollection.BuildServiceProvider();

        MainWindow = new MainWindow()
        {
            DataContext = ServiceProvider.GetRequiredService<MainViewModel>()
        };

        MainWindow.Show();

        base.OnStartup(e);
    }

    static private void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IConfiguration>(configuration);
        services.AddSingleton<IViewsRoute, ViewsRoute>();
        services.AddScoped<ICustomerService, CustomerService>();

        services.AddSingleton<LoadingViewModel>();
        services.AddSingleton<NewCustomerViewModel>();
        services.AddSingleton<MainViewModel>();
    }
}
