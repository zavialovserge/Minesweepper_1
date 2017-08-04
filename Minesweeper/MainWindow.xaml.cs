using Minesweeper.ViewModel;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Minesweeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel viewModel { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel = ((ViewModelLocator)DataContext).Main;
        }
    }
}
