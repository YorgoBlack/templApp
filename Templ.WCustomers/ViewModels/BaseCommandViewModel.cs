using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace Templ.WCustomers.ViewModels;
using Shared;
using Models;
using System.ComponentModel;
using System;
using System.Collections;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using Templ.WCustomers.Services;

public class BaseCommandViewModel : BaseRoutedViewModel
{
    protected readonly MainViewModel _mainViewModel;
    protected readonly ICustomerService _customerService;
    public CustomerViewModel Customer { get; set; }
    public bool IsEnabled { get; set; } = true;

    public ICommand CancelCommand { get; set; }

    public BaseCommandViewModel(MainViewModel mainViewModel, ICustomerService customerService) 
        : this(mainViewModel, customerService, new CustomerViewModel(customerService), true)
    {
    }
        
    public BaseCommandViewModel(MainViewModel mainViewModel, ICustomerService customerService, CustomerViewModel customer, bool isEnabled = true) 
        : base(mainViewModel.ViewsRoute)
    {
        _customerService = customerService;
        _mainViewModel = mainViewModel;
        Customer = customer;
        IsEnabled = isEnabled;
        OnPropertyChanged(nameof(IsEnabled));

        CancelCommand = new RelayCommand(() =>
        {
            ViewsRoute.Route(null);
        });
    }
}