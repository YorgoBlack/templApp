using Microsoft.Extensions.Configuration;
using System.Windows;

namespace templ.WpfClient;
using Services;

public partial class MainWindow : Window
{
    readonly MainViewModel model;
    readonly IService _customerService;

    public MainWindow(IService customerService)
    {
        _customerService = customerService;
        model = new MainViewModel(customerService);

        InitializeComponent();
        Loaded += MainWindow_Loaded;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        DataContext = model;
        _customerService.QueryCustomers();
    }
}
