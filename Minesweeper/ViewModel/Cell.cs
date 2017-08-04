using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Minesweeper.Properties;
using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows;
using System.Windows.Input;

namespace Minesweeper.ViewModel
{
    public class Cell: ViewModelBase
    {
        private string _imageUri;

        public string imageUri
        {
            get { return _imageUri; }
            set
            {
                _imageUri = value;
                RaisePropertyChanged();
            }
        }
        private bool flag;

        public bool Flag
        {
            get { return flag; }
            set { flag = value; }
        }
        private bool isBomb;

        public bool IsBomb
        {
            get { return isBomb; }
            set { isBomb = value; }
        }
        public int bombCounter { get; set; }
        private bool isEnable;

        public bool IsEnable
        {
            get { return isEnable; }
            set
            {
                isEnable = value;
                RaisePropertyChanged();
            }
        }
        private bool isOpen;
        public bool IsOpen
        {
            get { return isOpen; }
            set
            {
                isOpen = value;
                RaisePropertyChanged();
            }
        }
       

        public Game Game { get; set; }
        public Cell(Game game)
        {
            this.Game = game;
            IsEnable = true;
            this.imageUri = @"C:\Users\qix07414f2\Desktop\Minesweeper\Minesweeper\Resource\Image\space.png";
            leftClick = new RelayCommand(ChangeUri,()=>!flag);
            rightClick = new RelayCommand(ChangeFlagUri,()=>!isOpen);
            middleClick = new RelayCommand<Cell>(Game.OpenCells, Game.canOpenNearCells);
            bombCounter = 0;
        }

        private void ChangeFlagUri()
        {
            flag = flag ? false : true;
            if (!isOpen)
            {
                this.imageUri = flag ? @"C:\Users\qix07414f2\Desktop\Minesweeper\Minesweeper\Resource\Image\flag.png"
                                 : @"C:\Users\qix07414f2\Desktop\Minesweeper\Minesweeper\Resource\Image\space.png";
            }
            
            if (flag && Game.BombCounter!=0 && !IsOpen)
            {
                Game.BombCounter--;
            }

            else if(!IsOpen)
            {
                Game.BombCounter++;
            }

            bool isWin = false;
            if (Game.BombCounter == 0)
            {

                isWin = true;
                foreach (var cell in Game.cells)
                {

                    if ((!cell.Flag && cell.isBomb) || (cell.Flag && !cell.isBomb))
                    {
                        isWin = false;
                        break;
                    }
                }
                    
            }
            if (isWin)
            {
                
                for(int i = 0; i < Game.cells.Count; i++)
                {
                    if (!Game.cells[i].flag )
                    {
                        ChangeImage(Game.cells[i]);
                    }
                }
                MessageBox.Show("Поздравляю Вы выиграли!!! Ваше время " + Game.time);
                Settings.Default.UserRecord = Game.time;
                Settings.Default.Save();
                Game.Timer.Stop();
                Game.viewModel.StartNewGame();
                return;
            }
           
        }
        public void ChangeUri()
        {
            if (Game.Timer == null)
            {
                Game.Timer = new Timer() { Interval = 1000 };
                DateTime dateTime = new DateTime();
                Game.Timer.Elapsed += (sender, args) =>
                {
                    dateTime = dateTime.AddSeconds(1);
                    Game.time = $"{dateTime.Hour.ToString("D2")}:{dateTime.Minute.ToString("D2")}:{dateTime.Second.ToString("D2")}";
                    RaisePropertyChanged("time");
                };
                Game.Timer.Start();
            }
            if (isBomb)
            {
                imageUri = @"C:\Users\qix07414f2\Desktop\Minesweeper\Minesweeper\Resource\Image\explosion.png";
                foreach (var cell in Game.cells)
                {
                    cell.IsEnable = false;//stopGame
                    Game.Timer?.Stop();
                    if (cell.Flag && !cell.isBomb && cell != this)
                    {
                        cell.imageUri = @"C:\Users\qix07414f2\Desktop\Minesweeper\Minesweeper\Resource\Image\broken_flag.png";
                    }
                    if (!cell.Flag && cell.isBomb && cell != this)
                    {
                        cell.imageUri = @"C:\Users\qix07414f2\Desktop\Minesweeper\Minesweeper\Resource\Image\bomb.png";
                    }
                   
                    
                }
            }
            else
            {
                foreach (var cell in Game.cells)
                {
                    if ((!cell.Flag && cell.isBomb) || (cell.Flag && !cell.isBomb))
                    {
                        Game.IsWin = false;
                    }
                }
                
                if (Game.BombCounter == 0)
                {
                    
                    for (int i = 0; i < Game.cells.Count; i++)
                    {
                        if (!Game.cells[i].isOpen && !Game.cells[i].flag && !Game.cells[i].IsBomb)
                        {
                            Game.cells[i].imageUri = ChangeImage(Game.cells[i]);
                        }
                    }
                    if (Game.IsWin)
                    {
                        return;

                    }
                    else
                    {
                        MessageBox.Show("Поздравляю Вы выиграли!!! Ваше время " + Game.time);
                        Settings.Default.UserRecord = Game.time;
                        Settings.Default.Save();
                        Game.viewModel.StartNewGame();
                         return;
                    }
                   
                   
                }
                IsOpen = true;
                this.imageUri = ChangeImage(this);
              
            }
           
            
        }

        private string ChangeImage(Cell cell)
        {
           
            string newUri = string.Empty;
            switch (cell.bombCounter)
            {
                case 0:
                    newUri = @"C:\Users\qix07414f2\Desktop\Minesweeper\Minesweeper\Resource\Image\" + 0 + ".png";
                    Game.OpenCells(this);
                    break;
                case 1:
                    newUri = @"C:\Users\qix07414f2\Desktop\Minesweeper\Minesweeper\Resource\Image\" + 1 + ".png";
                    break;
                case 2:
                    newUri = @"C:\Users\qix07414f2\Desktop\Minesweeper\Minesweeper\Resource\Image\" + 2 + ".png";
                    break;
                case 3:
                    newUri = @"C:\Users\qix07414f2\Desktop\Minesweeper\Minesweeper\Resource\Image\" + 3 + ".png";
                    break;
                case 4:
                    newUri = @"C:\Users\qix07414f2\Desktop\Minesweeper\Minesweeper\Resource\Image\" + 4 + ".png";
                    break;
                case 5:
                    newUri = @"C:\Users\qix07414f2\Desktop\Minesweeper\Minesweeper\Resource\Image\" + 5 + ".png";
                    break;
                case 6:
                    newUri = @"C:\Users\qix07414f2\Desktop\Minesweeper\Minesweeper\Resource\Image\" + 6 + ".png";
                    break;
                case 7:
                    newUri = @"C:\Users\qix07414f2\Desktop\Minesweeper\Minesweeper\Resource\Image\" + 7 + ".png";
                    break;
                case 8:
                    newUri = @"C:\Users\qix07414f2\Desktop\Minesweeper\Minesweeper\Resource\Image\" + 8 + ".png";
                    break;
            }
            return newUri;
        }

        public ICommand LeftClick => leftClick;
        private RelayCommand leftClick;
        public ICommand RightClick => rightClick;
        private RelayCommand rightClick;

        public ICommand MiddleClick => middleClick;
        private RelayCommand<Cell> middleClick;
    }
}
