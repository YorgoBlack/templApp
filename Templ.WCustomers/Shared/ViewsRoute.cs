using System;

namespace Templ.WCustomers.Shared;

public class ViewsRoute : IViewsRoute
{
    public BaseRoutedViewModel? CurrentViewModel { get; private set; }

    public event EventHandler? WhenRoute;

    protected void OnRoute()
    {
        WhenRoute?.Invoke(this, EventArgs.Empty);
    }

    public void Route(BaseRoutedViewModel? ViewModel)
    {
        if (CurrentViewModel != ViewModel)
        {
            CurrentViewModel?.Dispose();
            CurrentViewModel = ViewModel;
            OnRoute();
        }
    }
}
