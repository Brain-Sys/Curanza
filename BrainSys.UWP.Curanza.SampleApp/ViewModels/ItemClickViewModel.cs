
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Windows.UI.Popups;

namespace BrainSys.UWP.Curanza.SampleApp.ViewModels
{
    public class ItemClickViewModel : ApplicationViewModelBase
    {
        Random random = new Random((int)DateTime.Now.Ticks);

        private ObservableCollection<Software> items;
        public ObservableCollection<Software> Items
        {
            get { return items; }
            set { items = value; }
        }

        public RelayCommand<Software> GetDetailCommand { get; set; }

        public ItemClickViewModel()
        {
            this.Items = new ObservableCollection<Software>();
            this.GetDetailCommand = new RelayCommand<Software>(getDetailCommandExecute);

            for (int i = 1; i <= 50; i++)
            {
                int download = random.Next(1000, 20000);
                this.Items.Add(new Software("Software Name " + i, "ver. " + i)
                    { Download = download });
            }
        }

        private async void getDetailCommandExecute(Software obj)
        {
            string message = string.Format("This software has been downloaded {0} times.", obj.Download);
            MessageDialog dlg = new MessageDialog(message);
            await dlg.ShowAsync();
        }
    }

    public class Software : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; this.RaisePropertyChanged("Name"); }
        }

        private string version;
        public string Version
        {
            get { return version; }
            set { version = value; this.RaisePropertyChanged("Version"); }
        }

        private int download;
        public int Download
        {
            get { return download; }
            set { download = value; this.RaisePropertyChanged("Download"); }
        }

        public Software(string name, string version)
        {
            this.Name = name;
            this.Version = version;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}