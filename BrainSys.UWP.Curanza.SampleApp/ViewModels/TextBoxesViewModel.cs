using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace BrainSys.UWP.Curanza.SampleApp.ViewModels
{
    public class TextBoxesViewModel
    {
        public RelayCommand Command { get; set; }
        public RelayCommand RegexCommand { get; set; }
        public TextBoxesViewModel()
        {
            this.Command = new RelayCommand(async () =>
            {
                MessageDialog dlg = new MessageDialog("Command Executed!");
                await dlg.ShowAsync();
            });

            this.RegexCommand = new RelayCommand(async () =>
            {
                MessageDialog dlg = new MessageDialog("Regex Command Executed!");
                await dlg.ShowAsync();
            });
        }
    }
}