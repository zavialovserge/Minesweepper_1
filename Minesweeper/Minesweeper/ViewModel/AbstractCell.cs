using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.ViewModel
{
    public abstract class AbstractCell: ViewModelBase
    {
        /// <summary>
        /// ImageUri path
        /// </summary>
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
        /// <summary>
        /// Flag state
        /// </summary>
        private bool flag;

        public bool Flag
        {
            get { return flag; }
            set { flag = value; }
        }
        /// <summary>
        /// Bomb state
        /// </summary>
        private bool isBomb;

        public bool IsBomb
        {
            get { return isBomb; }
            set { isBomb = value; }
        }

        public int bombCounter { get; set; }
        /// <summary>
        /// IsEnable state
        /// </summary>
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
        /// <summary>
        /// Open cell state
        /// </summary>
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
    }
}
