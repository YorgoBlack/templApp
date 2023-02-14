using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;

namespace Templ.WCustomers.ViewModels;
using Shared;
using Models;
using Services;
using System;
using Helpers;

public class MainViewModel : BaseRoutedViewModel
{
    private readonly ICustomerService _customerService;

    public NavigationViewModel Navigation { get; set; }

    public ICommand ShowNewCustomerCommand { get; set; }
    
    public ICommand ShowEditCustomerCommand { get; set; }

    public ICommand ShowDeleteCustomerCommand { get; set; }

    public ICommand FilterCommand { get; set; }

    public ICommand TopCommand { get; set; }

    public ICommand PrevCommand { get; set; }

    public ICommand NextCommand { get; set; }

    public ObservableCollection<Customer> Customers { get; set; } = null!;

    public MainViewModel(IViewsRoute viewsRoute, ICustomerService customerService) : base(viewsRoute)
    {
        _customerService = customerService;
        Navigation = new(this);

        ShowNewCustomerCommand = new RelayCommand( () => 
        {
            ViewsRoute.Route( new NewCustomerViewModel(this,_customerService) );
        });

        ShowEditCustomerCommand = new RelayCommand<Customer>((customer) =>
        {
            if( customer == null ) throw new ArgumentNullException(nameof(customer));

            ViewsRoute.Route(new UpdateCustomerViewModel(this,_customerService, customer));
        });

        ShowDeleteCustomerCommand = new RelayCommand<Customer>((customer) =>
        {
            if (customer == null) throw new ArgumentNullException(nameof(customer));

            ViewsRoute.Route(new DeleteCustomerViewModel(this, _customerService, customer));
        });

        FilterCommand = new RelayCommand<FilterCommandAgruments>(async (filter) => {
            if( filter != null )
                await RefreshCustomersAsync(Navigation.QueryParams(filter));
        });

        TopCommand = new RelayCommand(async () => {
            await RefreshCustomersAsync(Navigation.QueryParams("top"));
        });

        PrevCommand = new RelayCommand(async () => {
            await RefreshCustomersAsync(Navigation.QueryParams("prev"));
        });

        NextCommand = new RelayCommand(async () => {
            await RefreshCustomersAsync(Navigation.QueryParams("next"));
        });

        Task.Run(async () => 
            await RefreshCustomersAsync(Navigation.QueryParams()));
    }
    public async Task RefreshCustomersAsync()
        => await RefreshCustomersAsync(Navigation.QueryParams());

    public async void RefreshCustomers()
    {
        await Dispatcher.CurrentDispatcher.InvokeAsync(async () => {
            await RefreshCustomersAsync(Navigation.QueryParams());
        });
    }

    private async Task RefreshCustomersAsync(CustomerQueryParams queryParams)
    {
        ViewsRoute.Route(new LoadingViewModel(ViewsRoute));
        try
        {
            // TODO responsiveness debug
            await Task.Delay(2000);
            var dtos = await _customerService.QueryCustomersAsync(queryParams);

            Navigation.IsHasRecords = !dtos.IsLastRecords; 

            Customers?.Clear();
            Customers = new ObservableCollection<Customer>(dtos.Records
                .Select(c => new Customer(c.Id, c.Name, c.CompanyName, c.CompanyAddress, c.Phone, c.Email)));

            OnPropertyChanged(nameof(Customers));

            ViewsRoute.Route(null);
        }
        catch (Exception ex)
        {
            ViewsRoute.Route(new AlertViewModel(ViewsRoute, ex.Message));
        }
    }
}