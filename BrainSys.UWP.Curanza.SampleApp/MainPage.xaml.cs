using BrainSys.UWP.Curanza.SampleApp.Helpers;
using Windows.UI.Xaml.Controls;

namespace BrainSys.UWP.Curanza.SampleApp
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ApplicationContext.Dispatcher = this.Dispatcher;
        }
    }
}