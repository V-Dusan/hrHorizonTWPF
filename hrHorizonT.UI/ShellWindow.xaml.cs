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
        private MainViewModel _viewModel1;

        public ShellWindow(MainViewModel viewModel, MainViewModel viewModel1)
        {
            InitializeComponent();
            this._viewModel = viewModel;
            this._viewModel1 = viewModel1;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainWindow newMainWindow = new MainWindow(_viewModel);
            newMainWindow.Show();
        }

        private void MenuItem_Test(object sender, RoutedEventArgs e)
        {
            TestWindow newTestWindow = new TestWindow(_viewModel1);
            newTestWindow.Show();
        }
    }
}
