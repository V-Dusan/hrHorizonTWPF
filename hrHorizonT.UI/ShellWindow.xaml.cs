using hrHorizonT.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace hrHorizonT.UI
{
    /// <summary>
    /// Interaction logic for ShellWindow.xaml
    /// </summary>
    public partial class ShellWindow : Window
    {
        private MainViewModel _viewModel;

        public ShellWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            this._viewModel = viewModel; 
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainWindow newMainWindow = new MainWindow(_viewModel);
            newMainWindow.Show();
        }
    }
}
