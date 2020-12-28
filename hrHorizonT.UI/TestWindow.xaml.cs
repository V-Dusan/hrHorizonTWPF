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

    public partial class TestWindow : Window
    {
        private MainViewModel _viewModel1;

        public TestWindow(MainViewModel viewModel1)
        {
            InitializeComponent();
            _viewModel1 = viewModel1;
            DataContext = _viewModel1;
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel1.LoadAsync();
        }
    }
}
