using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainSys.UWP.Curanza.SampleApp.ViewModels
{
    public class WheelViewModel : ApplicationViewModelBase
    {
        private int quantity;
        public int Quantity
        {
            get { return quantity; }
            set { quantity = value; base.RaisePropertyChanged(); }
        }

        public RelayCommand<string> UpCommand { get; set; }
        public RelayCommand<string> DownCommand { get; set; }

        public WheelViewModel()
        {
            this.UpCommand = new RelayCommand<string>((x) => { this.Quantity += Int32.Parse(x); });
            this.DownCommand = new RelayCommand<string>((x) => { this.Quantity -= Int32.Parse(x); });
        }
    }
}