using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Templ.WCustomers.Views;

using CommunityToolkit.Mvvm.Input;
using Helpers;
using Shared;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

public partial class FilteredGrid : DataGrid
{
    public static DependencyProperty FilterCommandProperty
           = DependencyProperty.Register(
               "FilterCommand",
               typeof(ICommand),
               typeof(FilteredGrid));

    public RelayCommand<FilterCommandAgruments> FilterCommand
    {
        get
        {
            return (RelayCommand<FilterCommandAgruments>) GetValue(FilterCommandProperty);
        }

        set
        {
            SetValue(FilterCommandProperty, value);
        }
    }

    public FilteredGrid()
    {
        InitializeComponent();
    }

    private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (e.Key == System.Windows.Input.Key.Enter && sender is DependencyObject dp)
        {
            var header = VisualTreeHelpers.FindAncestor<DataGridColumnHeader>(dp);
            if (header != null
                && header.Column is DataGridTextColumn column
                && column.Binding is System.Windows.Data.Binding binding)
            {
                var path = binding.Path.Path;
                FilterCommand?.Execute(new FilterCommandAgruments(path,(sender as TextBox)?.Text));
            }
        }
    }
}
