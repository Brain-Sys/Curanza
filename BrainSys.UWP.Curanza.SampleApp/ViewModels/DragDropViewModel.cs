using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace BrainSys.UWP.Curanza.SampleApp.ViewModels
{
    public class DragDropViewModel : ApplicationViewModelBase
    {
        Random random = new Random((int)DateTime.Now.Ticks);

        private Person currentPerson;
        public Person CurrentPerson
        {
            get { return currentPerson;; }
            set { currentPerson = value; base.RaisePropertyChanged(); }
        }

        private ObservableCollection<Person> people;
        public ObservableCollection<Person> People
        {
            get { return people; }
            set { people = value; base.RaisePropertyChanged(); }
        }

        private ObservableCollection<Person> destination;
        public ObservableCollection<Person> Destination
        {
            get { return destination; }
            set { destination = value; base.RaisePropertyChanged(); }
        }

        private string dragDropMessage;
        public string DragDropMessage
        {
            get { return dragDropMessage; }
            set { dragDropMessage = value; base.RaisePropertyChanged(); }
        }

        public RelayCommand<Person> DragCommand { get; set; }
        public RelayCommand DropCommand { get; set; }

        public DragDropViewModel()
        {
            this.DragDropMessage = "waiting";
            this.DragCommand = new RelayCommand<Person>(dragCommandExecute, dragCommandCanExecute);
            this.DropCommand = new RelayCommand(dropCommandExecute, dropCommandCanExecute);
            this.People = new ObservableCollection<Person>();
            this.Destination = new ObservableCollection<Person>();

            for (int i = 1; i <= 50; i++)
            {
                int age = random.Next(10, 90);
                this.People.Add(new Person("First Name " + i, "Last Name " + i, age));
            }
        }

        private bool dropCommandCanExecute()
        {
            if (this.CurrentPerson == null)
            {
                this.DragDropMessage = "you cannot drop here";
                return false;
            }

            bool result = this.CurrentPerson.Moved == false;

            if (result)
            {
                this.DragDropMessage = "you can drop here";
            }
            else
            {
                this.DragDropMessage = "you cannot drop here";
            }

            return result;
        }

        private void dropCommandExecute()
        {
            if (this.CurrentPerson == null) return;

            Person clone = this.CurrentPerson.Clone();
            this.Destination.Add(clone);
            this.CurrentPerson.Moved = true;
            this.DragDropMessage = "waiting";
        }

        private void dragCommandExecute(Person obj)
        {
            if (obj == null) return;
            this.CurrentPerson = obj;
            this.DragDropMessage = string.Format("Dragging '{0}'", this.CurrentPerson.ToString());
        }

        private bool dragCommandCanExecute(Person obj)
        {
            if (obj == null)
            {
                this.DragDropMessage = "you cannot drop here";
                return false;
            }

            bool result = obj.Age >= 18;

            if (!result)
            {
                this.DragDropMessage = "you cannot drag";
            }

            return result;
        }
    }

    public class Person : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; this.RaisePropertyChanged("FirstName"); }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; this.RaisePropertyChanged("LastName"); }
        }

        private int age;
        public int Age
        {
            get { return age; }
            set { age = value; this.RaisePropertyChanged("Age"); }
        }

        private bool moved;
        public bool Moved
        {
            get { return moved; }
            set { moved = value; this.RaisePropertyChanged("Moved"); }
        }

        public Person(string firstName, string lastName, int age)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Age = age;
            this.Moved = false;
        }

        public Person Clone()
        {
            Person p = new Person(this.FirstName, this.LastName, this.Age);
            p.Moved = this.Moved;

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
            return string.Format("{0} {1}", this.FirstName, this.LastName);
        }
    }

    public class MovedToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Brush result = new SolidColorBrush(Windows.UI.Colors.Transparent);
            bool blnValue = (bool)value;

            if (blnValue)
            {
                result = new SolidColorBrush(Windows.UI.Colors.Red);
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}