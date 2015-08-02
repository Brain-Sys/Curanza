using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace BrainSys.UWP.Curanza.SampleApp.ViewModels
{
    class SwipeViewModel : ApplicationViewModelBase
    {
        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; base.RaisePropertyChanged(); }
        }

        private string additionalMessage;
        public string AdditionalMessage
        {
            get { return additionalMessage; }
            set { additionalMessage = value; base.RaisePropertyChanged(); }
        }

        public RelayCommand<string> LeftCommand { get; set; }
        public RelayCommand<string> RightCommand { get; set; }
        public RelayCommand<string> UpCommand { get; set; }
        public RelayCommand<string> DownCommand { get; set; }

        public SwipeViewModel()
        {
            this.Message = "please swipe with your finger";
            this.LeftCommand = new RelayCommand<string>((s) => { this.Message = "Swipe Left"; this.AdditionalMessage = s; });
            this.RightCommand = new RelayCommand<string>((s) => { this.Message = "Swipe Right"; this.AdditionalMessage = s; });
            this.UpCommand = new RelayCommand<string>((s) => { this.Message = "Swipe Up"; this.AdditionalMessage = s; });
            this.DownCommand = new RelayCommand<string>((s) => { this.Message = "Swipe Down"; this.AdditionalMessage = s; });
        }
    }
}