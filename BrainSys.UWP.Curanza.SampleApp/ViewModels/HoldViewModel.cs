using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BrainSys.UWP.Curanza.SampleApp.Helpers;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BrainSys.UWP.Curanza.SampleApp.ViewModels
{
    public class HoldViewModel : ApplicationViewModelBase
    {
        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; base.RaisePropertyChanged(); }
        }

        public RelayCommand Command { get; set; }

        public HoldViewModel()
        {
            this.Command = new RelayCommand(commandExecute);
        }

        private void commandExecute()
        {
            this.Message = "Hold Command Executed";

            Timer timer = new Timer(
                new TimerCallback(async (o) =>
                {
                    await ApplicationContext.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { this.Message = string.Empty; });
                }), null, 3000, Timeout.Infinite);
        }
    }
}