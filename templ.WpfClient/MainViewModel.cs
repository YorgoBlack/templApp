using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace templ.WpfClient;
using Models;
using Services;
using System.Linq;
using System.Runtime.CompilerServices;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly IService _customerService;

    public MainViewModel()
    {
        _customerService = null!;
    }

    public MainViewModel(IService customerService)
    {
        _customerService = customerService;

        _customerService!.CustomersLoadedEvent += (sender, customers) => {


            //Customers?.Clear();
            //var _l = customers.Select(x => new CustomerViewModel()).ToList();
            //Customers = new ObservableCollection<CustomerViewModel>(_l);
            Loaded = true;
            OnPropertyChanged(nameof(Loaded));
        };
    }

    public int PageNo { get; set; } = 1;

    public bool Loaded { get; set; } = false;

    public ObservableCollection<CustomerViewModel> Customers { get; set; } = null!;

    

    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string prop = "") => 
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
}
