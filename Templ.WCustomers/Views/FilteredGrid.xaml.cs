using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Templ.WCustomers.Views;
using Helpers;

public partial class FilteredGrid : DataGrid
{
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
            }
        }
    }

}
