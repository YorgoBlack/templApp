
namespace Templ.WCustomers.ViewModels;

using CommunityToolkit.Mvvm.Input;
using Models;
using Shared;
using System;
using System.Windows.Input;
using Templ.WCustomers.Services;

public class DeleteCustomerViewModel : BaseCommandViewModel
{
    public ICommand DeleteCommand { get; set; }

    public DeleteCustomerViewModel(MainViewModel mainViewModel, ICustomerService customerService, Customer customer) 
        : base(mainViewModel, customerService, new CustomerViewModel(customerService).Set(customer), false)
    {
        DeleteCommand = new RelayCommand<CustomerViewModel>(async (customer) =>
        {
            await customerService .DeleteAsync(Guid.Parse(customer!.Id));
            ViewsRoute.Route(null);
            await _mainViewModel.RefreshCustomersAsync();
        });

    }
}
