
using System.Collections.Generic;
using System.Linq;
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

    public static IEnumerable<T> FindVisualChilds<T>(DependencyObject depObj) where T : DependencyObject
    {
        if (depObj == null) yield return (T)Enumerable.Empty<T>();
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
        {
            DependencyObject ithChild = VisualTreeHelper.GetChild(depObj, i);
            if (ithChild == null) continue;
            if (ithChild is T t) yield return t;
            foreach (T childOfChild in FindVisualChilds<T>(ithChild)) yield return childOfChild;
        }
    }
}
