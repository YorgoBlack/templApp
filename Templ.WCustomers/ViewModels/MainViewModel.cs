using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Templ.WCustomers.ViewModels;
using Shared;
using Models;
using Services;
using System;
using System.Windows.Threading;
using System.Windows;

public class MainViewModel : BaseRoutedViewModel
{
    private readonly ICustomerService _customerService;
    private readonly CustomerQueryParams _customerQueryParams = new();

    public ICommand ShowNewCustomerCommand { get; set; }
    
    public ICommand ShowEditCustomerCommand { get; set; }

    public ICommand ShowDeleteCustomerCommand { get; set; }

    public ICommand RefreshCustomersCommand { get; set; }

    public ICommand TopCommand { get; set; }

    public ICommand PrevCommand { get; set; }

    public ICommand NextCommand { get; set; }


    public ObservableCollection<Customer> Customers { get; set; } = null!;

    public int _pageSizeIndex;
    public int PageSize { 
        get => _pageSizeIndex;
        set {
            _pageSizeIndex = value;
            PageSizeChange();
        } 
    }

    public int _pageNo = 1;
    public int PageNo
    {
        get => _pageNo;
        set
        {
            _pageNo = value;
        }
    }

    public MainViewModel(IViewsRoute viewsRoute, ICustomerService customerService) : base(viewsRoute)
    {
        _customerService = customerService;

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

        TopCommand = new RelayCommand(async () => {
            BuilQueryParams("top");
            await RefreshCustomersAsync();
        });

        PrevCommand = new RelayCommand(async () => {
            BuilQueryParams("prev");
            await RefreshCustomersAsync();
        });

        NextCommand = new RelayCommand(async () => {
            BuilQueryParams("next");
            await RefreshCustomersAsync();
        });

        RefreshCustomersCommand = new RelayCommand(async () => await RefreshCustomersAsync() );

        OnPropertyChanged(nameof(PageSize));
        OnPropertyChanged(nameof(PageNo));

        Task.Run(async () => await RefreshCustomersAsync() );
    }

    public async Task RefreshCustomersAsync()
    {
        ViewsRoute.Route(new LoadingViewModel(ViewsRoute));
        try
        {
            await Task.Delay(2000);
            var dtos = await _customerService.QueryCustomersAsync(_customerQueryParams);
            Customers?.Clear();
            Customers = new ObservableCollection<Customer>(dtos
                .Select(c => new Customer(c.Id, c.Name, c.CompanyName, c.CompanyAddress, c.Phone, c.Email)));

            OnPropertyChanged(nameof(Customers));

            ViewsRoute.Route(null);
        }
        catch (Exception ex)
        {
            ViewsRoute.Route(new AlertViewModel(ViewsRoute, ex.Message));
        }
    }

    private async void PageSizeChange()
    {
        await Dispatcher.CurrentDispatcher.InvokeAsync(async () => {
            BuilQueryParams("pageSize");
            await RefreshCustomersAsync();
        });
    }

    bool BuilQueryParams(string action)
    {
        switch (action)
        {
            case "top":
                _customerQueryParams.Page = 1;
                return true;

            case "next":
                _customerQueryParams.Page++;
                return true;
                

            case "prev":
                if (_customerQueryParams.PageSize > 1) _customerQueryParams.Page--;
                return true;

            case "pagesize":
                _customerQueryParams.PageSize = PageSize;
                return true;

            default: 
                break;
        }

        return false;

    }

}