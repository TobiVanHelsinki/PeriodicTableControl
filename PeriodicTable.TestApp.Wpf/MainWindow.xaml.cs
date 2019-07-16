using System.Windows;
using System.Windows.Controls;

namespace PeriodicTable.TestApp.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Single_Selected(object sender, RoutedEventArgs e)
        {
            PSE.SelectionMode = SelectionMode.Single;
        }

        private void Multiple_Selected(object sender, RoutedEventArgs e)
        {
            PSE.SelectionMode = SelectionMode.Multiple;
        }

        private void Extendet_Selected(object sender, RoutedEventArgs e)
        {
            PSE.SelectionMode = SelectionMode.Extended;
        }
    }
}
