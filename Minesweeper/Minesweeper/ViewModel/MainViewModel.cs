using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using Minesweeper.Properties;
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
            ImageResourse = new Dictionary<ResourceImage, string>();
            SetResources();
            Settings = new SettingsViewModel(this);
            Game = new Game(this);
            newGame = new RelayCommand(StartNewGame);
        }

        private void SetResources()
        {
            var s = Resources.ResourceManager;
            ImageResourse.Add(ResourceImage.zero, Environment.CurrentDirectory + @"\Resource\Image\"+ "0" + ".png");
            ImageResourse.Add(ResourceImage.one, Environment.CurrentDirectory + @"\Resource\Image\" + "1" + ".png");
            ImageResourse.Add(ResourceImage.two, Environment.CurrentDirectory + @"\Resource\Image\" +"2" + ".png");
            ImageResourse.Add(ResourceImage.three, Environment.CurrentDirectory + @"\Resource\Image\" +"3" + ".png");
            ImageResourse.Add(ResourceImage.four, Environment.CurrentDirectory + @"\Resource\Image\" +"4" + ".png");
            ImageResourse.Add(ResourceImage.five, Environment.CurrentDirectory + @"\Resource\Image\" +"5" + ".png");
            ImageResourse.Add(ResourceImage.six, Environment.CurrentDirectory + @"\Resource\Image\" +"6" + ".png");
            ImageResourse.Add(ResourceImage.seven, Environment.CurrentDirectory + @"\Resource\Image\" +"7" + ".png");
            ImageResourse.Add(ResourceImage.eight, Environment.CurrentDirectory + @"\Resource\Image\" +"8" + ".png");
            ImageResourse.Add(ResourceImage.flag, Environment.CurrentDirectory + @"\Resource\Image\" +"flag" + ".png");
            ImageResourse.Add(ResourceImage.broken_flag, Environment.CurrentDirectory + @"\Resource\Image\" +"broken_flag" + ".png");
            ImageResourse.Add(ResourceImage.bomb, Environment.CurrentDirectory + @"\Resource\Image\" +"bomb" + ".png");
            ImageResourse.Add(ResourceImage.space, Environment.CurrentDirectory + @"\Resource\Image\" +"space" + ".png");
            ImageResourse.Add(ResourceImage.explosion, Environment.CurrentDirectory + @"\Resource\Image\" + "explosion" + ".png");
        }

        public void StartNewGame()
        {
            Game = new Game(this);
            RaisePropertyChanged(nameof(Game));

        }

        public ICommand NewGame => newGame;
        public RelayCommand newGame;
        public Dictionary<ResourceImage, string> ImageResourse { get; set; }
    }
    public enum ResourceImage
    {
        zero,
        one,
        two,
        three,
        four,
        five,
        six,
        seven,
        eight,
        flag,
        broken_flag,
        bomb,
        explosion,
        space
    }
}