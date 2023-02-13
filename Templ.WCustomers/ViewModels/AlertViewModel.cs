
namespace Templ.WCustomers.ViewModels;

using CommunityToolkit.Mvvm.Input;
using Shared;
using System.Windows.Input;

public class AlertViewModel : BaseRoutedViewModel
{
    public ICommand CloseCommand { get; set; }

    public string Message { get; set; }

    public AlertViewModel(IViewsRoute viewsRoute, string message) : base(viewsRoute)
    {
        Message = message;

        CloseCommand = new RelayCommand(() => ViewsRoute.Route(null));
    }
}
