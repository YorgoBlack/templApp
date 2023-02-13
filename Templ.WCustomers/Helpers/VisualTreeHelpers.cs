
using System.Windows;
using System.Windows.Media;

namespace Templ.WCustomers.Helpers;

public static class VisualTreeHelpers
{
    public static T? FindAncestor<T>(DependencyObject current)
        where T : DependencyObject
    {
        current = VisualTreeHelper.GetParent(current);

        while (current != null)
        {
            if (current is T) return (T)current;

            current = VisualTreeHelper.GetParent(current);
        }

        return null;
    }

}
