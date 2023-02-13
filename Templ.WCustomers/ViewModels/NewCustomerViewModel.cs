namespace Templ.WCustomers.ViewModels;

using CommunityToolkit.Mvvm.Input;
using Shared;
using System;
using System.Windows.Input;
using Templ.WCustomers.Models;
using Templ.WCustomers.Services;

public class NewCustomerViewModel : BaseCommandViewModel
{
    public ICommand NewCustomerCommand { get; set; }

    public NewCustomerViewModel(MainViewModel mainViewModel, ICustomerService customerService) 
        : base(mainViewModel, customerService)
    {
        NewCustomerCommand = new RelayCommand<CustomerViewModel>(async (customer) =>
        {
            await customerService.CreateNewAsync(customer!.AsCustomer());
            ViewsRoute.Route(null);
            await _mainViewModel.RefreshCustomersAsync();
        });

    }
}
