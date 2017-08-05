using Minesweeper.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Minesweeper.View
{
    /// <summary>
    /// Interaction logic for SettingView.xaml
    /// </summary>
    public partial class SettingView : UserControl
    {
        public SettingsViewModel viewModel { get; set; }
        public SettingView()
        {
            InitializeComponent();
            //this.IsVisibleChanged += viewModel.CloseWindow;
        }


      

       

        private void DifficulComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var s = (ComboBox)sender;
            if (viewModel != null)
            {
                viewModel.Difficult = (Difficult)s.SelectedItem;
            }
            
        }
    }
}
