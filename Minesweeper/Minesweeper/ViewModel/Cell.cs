using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Minesweeper.Properties;
using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Linq;

namespace Minesweeper.ViewModel
{
    public class Cell: AbstractCell
    {
        /// <summary>
        /// Parent
        /// </summary>
        public Game Game { get; set; }
        public Cell(Game game)
        {
            this.Game = game;
            IsEnable = true;
            this.imageUri = Game.viewModel.ImageResourse[ResourceImage.space];
            leftClick = new RelayCommand(ChangeUri,()=>!Flag);
            rightClick = new RelayCommand(ChangeFlagUri,()=>!IsOpen);
            middleClick = new RelayCommand<Cell>(Game.OpenCells, Game.CanOpenNearCells);
            bombCounter = 0;
        }
        /// <summary>
        /// Change flag uri
        /// </summary>
        private void ChangeFlagUri()
        {
            if (Game.Timer == null)
            {
                StartTimer();
            }
            Flag = Flag ? false : true;
            if (!IsOpen)
            {
                this.imageUri = Flag ? Game.viewModel.ImageResourse[ResourceImage.flag]
                                     : Game.viewModel.ImageResourse[ResourceImage.space];
            }
            
            if (Flag && Game.BombCounter!=0 && !IsOpen)
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

                    if ((!cell.Flag && cell.IsBomb) || (cell.Flag && !cell.IsBomb))
                    {
                        isWin = false;
                        break;
                    }
                }
                    
            }
            if (isWin)
            {
                GameWin();
                return;
            }
           
        }
        /// <summary>
        /// Invoke when game is win
        /// Change all uri of cells that is not open
        /// </summary>
        private void GameWin()
        {
            for (int i = 0; i < Game.cells.Count; i++)
            {
                if (!Game.cells[i].IsOpen && !Game.cells[i].Flag && !Game.cells[i].IsBomb)
                {
                    Game.cells[i].imageUri = ChangeImage(Game.cells[i]);
                }
            }
            MessageBox.Show("Поздравляю Вы выиграли!!! Ваше время " + Game.time);
            Settings.Default.UserRecord = Game.time;
            Settings.Default.Save();
            Game.Timer.Stop();
            Game.viewModel.StartNewGame();
        }
        /// <summary>
        /// Change ImageUri of cell
        /// Stop game if this cell is bomb
        /// Stop game if win
        /// </summary>
        public void ChangeUri()
        {
            if (Game.Timer == null)
            {
                StartTimer();
            }
            if (IsBomb)
            {
                imageUri = Game.viewModel.ImageResourse[ResourceImage.explosion];
                foreach (var cell in Game.cells)
                {
                    cell.IsEnable = false;//stopGame
                    Game.Timer?.Stop();
                    if (cell.Flag && !cell.IsBomb && cell != this)
                    {
                        cell.imageUri = Game.viewModel.ImageResourse[ResourceImage.broken_flag];
                    }
                    if (!cell.Flag && cell.IsBomb && cell != this)
                    {
                        cell.imageUri = Game.viewModel.ImageResourse[ResourceImage.bomb];
                    }   
                }
            }
            else
            {
                IsOpen = true;
                int actualBombCounter = Game.cells.Where(x => !x.IsOpen).Count();
                bool allOpen = (Game.BombCounter - actualBombCounter) == 0;
                if (!allOpen)
                {
                    this.imageUri = ChangeImage(this);
                    return;
                }
                foreach (var cell in Game.cells)
                {
                    Game.IsWin = true;
                    if ((!cell.Flag && cell.IsBomb) || (cell.Flag && !cell.IsBomb))
                    {
                        Game.IsWin = false;
                        break;
                    }
                }
                if (Game.IsWin)
                {
                    GameWin();
                    return;
                }            
            }
            
        }
        /// <summary>
        /// Start game timer
        /// </summary>
        private void StartTimer()
        {
            Game.Timer = new Timer() { Interval = 1000 };
            DateTime dateTime = new DateTime();
            Game.Timer.Elapsed += (sender, args) =>
            {
                dateTime = dateTime.AddSeconds(1);
                Game.time = $"{dateTime.Hour.ToString("D2")}:{dateTime.Minute.ToString("D2")}:{dateTime.Second.ToString("D2")}";

            };
            Game.Timer.Start();
        }
        /// <summary>
        /// Change image of cell
        /// </summary>
        /// <param name="cell">Current cell</param>
        /// <returns></returns>
        private string ChangeImage(Cell cell)
        {
            string newUri = string.Empty;
            switch (cell.bombCounter)
            {
                case 0:
                    newUri = Game.viewModel.ImageResourse[ResourceImage.zero];
                    if (!Game.IsWin)
                    {
                        Game.OpenCells(this);
                    }
                    break;
                case 1:
                    newUri = Game.viewModel.ImageResourse[ResourceImage.one]; 
                    break;
                case 2:
                    newUri = Game.viewModel.ImageResourse[ResourceImage.two];
                    break;
                case 3:
                    newUri = Game.viewModel.ImageResourse[ResourceImage.three];
                    break;
                case 4:
                    newUri = Game.viewModel.ImageResourse[ResourceImage.four];
                    break;
                case 5:
                    newUri = Game.viewModel.ImageResourse[ResourceImage.five];
                    break;
                case 6:
                    newUri = Game.viewModel.ImageResourse[ResourceImage.six];
                    break;
                case 7:
                    newUri = Game.viewModel.ImageResourse[ResourceImage.seven];
                    break;
                case 8:
                    newUri = Game.viewModel.ImageResourse[ResourceImage.eight];
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
