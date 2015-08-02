using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace BrainSys.UWP.Curanza.SampleApp.ViewModels
{
    public class DoubleTappedViewModel : ApplicationViewModelBase
    {
        Random random = new Random((int)DateTime.Now.Ticks);

        private PhoneNumber currentPhoneNumber;
        public PhoneNumber CurrentPhoneNumber
        {
            get { return currentPhoneNumber; ; }
            set { currentPhoneNumber = value; base.RaisePropertyChanged(); }
        }

        private ObservableCollection<PhoneNumber> items;
        public ObservableCollection<PhoneNumber> Items
        {
            get { return items; }
            set { items = value; base.RaisePropertyChanged(); }
        }

        public RelayCommand<PhoneNumber> DeleteItemCommand { get; set; }
        public RelayCommand<PhoneNumber> EditItemCommand { get; set; }
        public RelayCommand<PhoneNumber> ConfirmItemCommand { get; set; }
        public RelayCommand<PhoneNumber> CallPhoneNumberCommand { get; set; }

        public DoubleTappedViewModel()
        {
            this.DeleteItemCommand = new RelayCommand<PhoneNumber>(deleteItemCommandExecute);
            this.EditItemCommand = new RelayCommand<PhoneNumber>(editItemCommandExecute);
            this.ConfirmItemCommand = new RelayCommand<PhoneNumber>(confirmItemCommandExecute);
            this.CallPhoneNumberCommand = new RelayCommand<PhoneNumber>(callPhoneNumberCommandExecute);
            this.Items = new ObservableCollection<PhoneNumber>();

            for (int i = 1; i <= 50; i++)
            {
                int number = random.Next(1000, 9999);
                this.Items.Add(new PhoneNumber("Phone Number n°" + i, "555-" + number));
            }
        }

        private async void callPhoneNumberCommandExecute(PhoneNumber obj)
        {
            if (obj == null) return;
            MessageDialog dlg = new MessageDialog("Calling phone number ... " + obj.Number);
            await dlg.ShowAsync();
        }

        private void confirmItemCommandExecute(PhoneNumber obj)
        {
            if (obj == null) return;
            obj.IsEditing = false;
        }

        private void editItemCommandExecute(PhoneNumber obj)
        {
            foreach(PhoneNumber pn in this.Items)
            {
                pn.IsEditing = false;
            }

            if (obj == null) return;
            obj.IsEditing = true;
        }

        private void deleteItemCommandExecute(PhoneNumber obj)
        {
            if (obj == null) return;
            this.Items.Remove(obj);
        }
    }

    public class PhoneNumber : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; this.RaisePropertyChanged("Description"); }
        }

        private string number;
        public string Number
        {
            get { return number; }
            set { number = value; this.RaisePropertyChanged("Number"); }
        }

        private bool isEditing;
        public bool IsEditing
        {
            get { return isEditing; }
            set { isEditing = value; this.RaisePropertyChanged("IsEditing");
                this.RaisePropertyChanged("IsNotEditing");
            }
        }

        public bool IsNotEditing
        {
            get
            {
                return !this.IsEditing;
            }
        }

        public PhoneNumber(string description, string number)
        {
            this.Description = description;
            this.Number = number;
        }

        public PhoneNumber Clone()
        {
            PhoneNumber p = new PhoneNumber(this.Description, this.Number);

            return p;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", this.Description, this.Number);
        }
    }
}