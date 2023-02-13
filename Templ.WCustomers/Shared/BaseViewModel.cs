using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Templ.WCustomers.Shared;

public class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    
    protected virtual void RaisePropertyChanged([CallerMemberName] string? PropertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? PropertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
    }
}
