using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace templ.WpfClient.UI;

public partial class TemplDataGrid : DataGrid
{
    public TemplDataGrid()
    {
        InitializeComponent();
    }

    private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if( e.Key == System.Windows.Input.Key.Enter  && sender is DependencyObject dp)
        {
            var header = VisualTreeHelpers.FindAncestor<DataGridColumnHeader>(dp);
            if( header != null 
                && header.Column is DataGridTextColumn column 
                && column.Binding is System.Windows.Data.Binding binding)
            {

                var path = binding.Path.Path;
            }
        }
        

        

    }

}
