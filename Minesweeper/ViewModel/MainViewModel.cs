using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;

namespace Minesweeper.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private Game game;

        public Game Game
        {
            get { return game; }
            set
            {
                game = value;
                RaisePropertyChanged(nameof(Game));

            }
        }
        private SettingsViewModel settings;

        public SettingsViewModel Settings
        {
            get { return settings; }
            set
            {
                settings = value;
                RaisePropertyChanged(nameof(Settings));

            }
        }
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            Settings = new SettingsViewModel(this);
            Game = new Game(this);           
            newGame = new RelayCommand(StartNewGame);
        }

        public void StartNewGame()
        {
            Game = new Game(this);
            RaisePropertyChanged(nameof(Game));

        }

        public ICommand NewGame => newGame;
        public RelayCommand newGame;
       
    }
}