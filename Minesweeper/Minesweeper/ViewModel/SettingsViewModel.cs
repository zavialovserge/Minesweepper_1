using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Minesweeper.Properties;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Minesweeper.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        /// <summary>
        /// Collection for ComboBox
        /// </summary>
        public ObservableCollection<Difficult> DifficultCollection { get; set; }
        public MainViewModel viewModel { get; set; }
        //Default count of cells
        public const int defaultCountOfCells = 6;
        //Default count of Difficut
        public const Difficult difficultDefault = Difficult.Medium;
        /// <summary>
        /// Current cells
        /// </summary>
        private int countOfCells;

        public int CountOfCells
        {
            get { return countOfCells; }
            set
            {
                countOfCells = value;
                RaisePropertyChanged();
            }
        }
        /// <summary>
        /// Current difficuk
        /// </summary>
        private Difficult difficult;

        public Difficult Difficult
        {
            get { return difficult; }
            set
            {
                difficult = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// User record
        /// </summary>
        private string userRecord;

        public string UserRecord
        {
            get { return userRecord; }
            set
            {
                userRecord = value;
                RaisePropertyChanged("UserRecord");
            }
        }
        private string visible;

        public string Visible
        {
            get { return visible; }
            set
            {
                visible = value;
                RaisePropertyChanged();
            }
        }
        /// <summary>
        /// Visible state
        /// </summary>
        private bool isVisible;

        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                isVisible = value;
            }
        }
       
        public SettingsViewModel(MainViewModel ViewModel)
        {
            viewModel = ViewModel;
            CountOfCells = Settings.Default.Number_of_cells;
            string temp = Settings.Default.Difficult;
            userRecord = Settings.Default.UserRecord;
            var tempArray = Enum.GetValues(typeof(Difficult));
            DifficultCollection = new ObservableCollection<Difficult>();
            foreach (var value in tempArray)
            {
                DifficultCollection.Add((Difficult)value);
                if (value.ToString() == temp)
                {
                    Difficult = (Difficult)value;
                }
            }
            Visible = "Collapsed";
            isVisible = false;
            
            changeSettings = new RelayCommand(NewSetting);
            resetSettings = new RelayCommand(Reset);

        }
        /// <summary>
        /// Reset setting to default values
        /// </summary>
        private void Reset()
        {
            Settings.Default.Number_of_cells = defaultCountOfCells; 
            Settings.Default.Difficult = difficultDefault.ToString();
            Settings.Default.Save();
           
            viewModel.Settings.Visible = "Collapsed";
           
            viewModel.StartNewGame();
            RaisePropertyChanged("Game");
            
        }
        /// <summary>
        /// Change settings to user settings
        /// </summary>
        private void NewSetting()
        {
            Settings.Default.Number_of_cells = countOfCells;
            Settings.Default.Difficult = difficult.ToString();
           
            Settings.Default.Save();
            
            viewModel.Settings.Visible = "Collapsed";
            
            viewModel.StartNewGame();
            RaisePropertyChanged("Game");

        }

        public ICommand ChangeSettings => changeSettings;
        private RelayCommand changeSettings;

        public ICommand ResetSettings => resetSettings;
        private RelayCommand resetSettings;
    }
    /// <summary>
    /// Different difficult
    /// </summary>
    public enum Difficult
    {
        Easy,
        Medium,
        Hard
    }
}
