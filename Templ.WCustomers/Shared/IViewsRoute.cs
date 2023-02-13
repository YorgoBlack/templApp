using System;

namespace Templ.WCustomers.Shared;

public interface IViewsRoute
{
    BaseRoutedViewModel? CurrentViewModel { get; }

    event EventHandler? WhenRoute;

    void Route(BaseRoutedViewModel? ViewModel);
}
