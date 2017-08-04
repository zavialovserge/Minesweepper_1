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
        public Action CloseWindow =>delegate { };
        public const int defaultCountOfCells = 6;
        public const Difficult difficultDefault = Difficult.Medium;
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
        private string userRecord;

        public string UserRecord
        {
            get { return userRecord; }
            set
            {
                userRecord = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Difficult> DifficultCollection { get; set; }
        public MainViewModel viewModel { get; set; }
        public SettingsViewModel(MainViewModel ViewModel)
        {
            viewModel = ViewModel;
            CountOfCells = Settings.Default.Number_of_cells;
            string temp = Settings.Default.Difficult;
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
            userRecord = Settings.Default.UserRecord;
            changeSettings = new RelayCommand(NewSetting);
            resetSettings = new RelayCommand(Reset);

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
        private bool isVisible;

        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                isVisible = value;
            }
        }
        private void Reset()
        {
            Settings.Default.Number_of_cells = defaultCountOfCells; 
            Settings.Default.Difficult = difficultDefault.ToString();
            Settings.Default.Save();
           
            viewModel.Settings.Visible = "Collapsed";
            object cork = new object();
            viewModel.StartNewGame();
            RaisePropertyChanged("Game");
            
        }
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
    public enum Difficult
    {
        Easy,
        Medium,
        Hard
    }
}
