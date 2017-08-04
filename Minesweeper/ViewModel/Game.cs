using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Minesweeper.Properties;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;
using System.Windows.Input;
using System.Windows.Threading;

namespace Minesweeper.ViewModel
{
    public class Game:ViewModelBase
    {
        public ObservableCollection<Cell> cells { get; set; }
        private int bombCounter;

        public int BombCounter
        {
            get { return bombCounter; }
            set {
                bombCounter = value;
                RaisePropertyChanged();
            }
        }
        
       

        public MainViewModel viewModel { get; set; }
        private Timer timer;

        public Timer Timer
        {
            get { return timer; }
            set { timer = value; }
        }
        private string Time;

        public string time
        {
            get { return Time; }
            set
            {
                Time = value;
                RaisePropertyChanged();
            }
        }
        private int globalCount;

        public int GlobalCount
        {
            get { return globalCount; }
            set
            {
                globalCount = Settings.Default.Number_of_cells;
                RaisePropertyChanged();
            }
        }

        public Game(MainViewModel ViewModel)
        {
            this.viewModel = ViewModel;
            cells = new ObservableCollection<Cell>();
            
            int GlobalCountPow = (int)Math.Pow(Settings.Default.Number_of_cells,2);
            for (int i = 0; i < GlobalCountPow; i++)
            {
                cells.Add(new Cell(this));
            }
            AddBomb();
            CountOfNearBombs();
            bombCounter= cells.Where(x => x.IsBomb).Count();
            showSettings = new RelayCommand(ShowSetting);
            
            IsEnable = true;
            Time = "00:00:00";
        }
        private bool isEnable;

        public bool IsEnable
        {
            get { return isEnable; }
            set {
                isEnable = value;
                RaisePropertyChanged();
            }
        }

        private bool isWin;

        public bool IsWin
        {
            get { return isWin; }
            set
            {
                isWin = value;
                RaisePropertyChanged();
            }
        }
        private void ShowSetting()
        {
            IsEnable = false ? true : false ;
            viewModel.Settings.IsVisible = false ? false : true;
            viewModel.Settings.Visible = viewModel.Settings.IsVisible ? "Visible"
                                                                      : "Collapsed";
            RaisePropertyChanged("Game");
            
        }

        public ICommand ShowSettings => showSettings;
        private RelayCommand showSettings;
        private void CountOfNearBombs()
        {
            int lenght = (int)Math.Sqrt(cells.Count) + 2;
            Cell[,] tempArray = InitializationArray(lenght);
            for (int i = 0; i < lenght; i++)
            {
                for (int j = 0; j < lenght; j++)
                {
                    for (int k = i - 1; k <= i + 1; k++)
                    {
                        for (int l = j - 1; l <= j + 1; l++)
                        {
                            if (tempArray[i, j].IsBomb)
                            {
                                tempArray[k, l].bombCounter++;
                            }
                        }
                    }
                }
            }
            UpdatingCells(tempArray, lenght);
            

        }

        private void UpdatingCells(Cell[,] tempArray,int lenght)
        {
            cells.Clear();
            for (int i = 0; i < lenght; i++)
            {
                for (int j = 0; j < lenght; j++)
                {
                    if (i != 0 && j != 0 && j != lenght - 1 && i != lenght - 1)
                    {
                        cells.Add(tempArray[i, j]);

                    }

                }
            }
        }

        private Cell[,] InitializationArray(int lenght)
        {
            Cell[,] tempArray=new Cell[lenght, lenght];
            int temp = 0;
            for (int i = 0; i < lenght; i++)
            {
                for (int j = 0; j < lenght; j++)
                {
                    if (i == 0 || j == 0 || j == lenght - 1 || i == lenght - 1)
                    {
                        tempArray[i, j] = new Cell(this) { bombCounter = Int32.MaxValue};
                    }
                    else
                    {
                        tempArray[i, j] = cells[temp];
                        temp++;
                    }
                }
            }
            return tempArray;
        }

        private void AddBomb()
        {
            double ver = 0;
            switch (Settings.Default.Difficult)
            {
                case "Easy":
                    ver = Settings.Default.Easy;
                    break;
                case "Medium":
                    ver = Settings.Default.Medium;
                    break;
                case "Hard":
                    ver = Settings.Default.Hard;
                    break;
            }
            int countOfBombs = (int)(ver * cells.Count);
            int index = 0;
            Random rand = new Random();
            for (int i = 0; i < countOfBombs; i++)
            {
                do
                {
                    index = rand.Next(0, cells.Count);
                } while (cells[index].IsBomb);
                cells[index].IsBomb = true;
            }
            

        }
        public void OpenCells(Cell cell)
        {
            int lenght = (int)Math.Sqrt(cells.Count) + 2;
            Cell[,] tempArray = InitializationArray(lenght);
            for (int i = 0; i < lenght; i++)
            {
                for (int j = 0; j < lenght; j++)
                {
                    if (tempArray[i,j]==cell)
                    {
                        for (int k = i-1; k <= i+1; k++)
                        {
                            for (int l = j-1; l <= j+1; l++)
                            {
                                if (!tempArray[k,l].IsOpen && !tempArray[k, l].Flag && !IsWin)
                                {
                                    tempArray[k, l].ChangeUri();
                                }
                            }
                        }
                    }
                }
            }
            UpdatingCells(tempArray, lenght);
        }
        public bool canOpenNearCells(Cell cell)
        {
            if (!cell.IsOpen)
            {
                return false;
            }
            int lenght = (int)Math.Sqrt(cells.Count) + 2;
            Cell[,] tempArray = InitializationArray(lenght);
            int counter = 0;
           
            for (int i = 0; i < lenght; i++)
            {
                for (int j = 0; j < lenght; j++)
                {
                    if (tempArray[i, j] == cell)
                    {
                        for (int k = i - 1; k <= i + 1; k++)
                        {
                            for (int l = j - 1; l <= j + 1; l++)
                            {
                                if (tempArray[k, l].Flag)
                                {
                                    counter++;
                                }
                            }
                        }
                    }
                }
            }
            return counter == cell.bombCounter;
        }
    }
}
