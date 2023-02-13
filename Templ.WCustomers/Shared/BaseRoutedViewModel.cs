using System;

namespace Templ.WCustomers.Shared;

public class BaseRoutedViewModel : BaseViewModel, IDisposable
{
    public IViewsRoute ViewsRoute { get; private set; }

    public BaseViewModel? CurrentViewModel => ViewsRoute.CurrentViewModel;

    public BaseRoutedViewModel(IViewsRoute viewsRoute)
    {
        ViewsRoute = viewsRoute;
        ViewsRoute.WhenRoute += HandleRoute;
    }

    protected virtual void HandleRoute(object? sender, EventArgs e)
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }
    public void Dispose()
    {
        ViewsRoute.WhenRoute -= HandleRoute;
        GC.SuppressFinalize(this);
    }
}
