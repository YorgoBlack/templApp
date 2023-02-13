using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace Templ.WCustomers.ViewModels;
using Shared;
using Models;
using Templ.WCustomers.Services;

public class UpdateCustomerViewModel : BaseCommandViewModel
{
    public ICommand UpdateCommand { get; set; }

    public UpdateCustomerViewModel(MainViewModel mainViewModel, ICustomerService customerService, Customer customer) 
        : base(mainViewModel, customerService, (new CustomerViewModel(customerService)).Set(customer) )
    {
        UpdateCommand = new RelayCommand<CustomerViewModel>(async (customer) =>
        {
            if ( !customer?.HasErrors == true )
            {
                await customerService.UpdateAsync(customer!.AsCustomer());
                ViewsRoute.Route(null);
                await _mainViewModel.RefreshCustomersAsync();
            }
        });

    }
}
