using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;

namespace BrainSys.UWP.Curanza.CommandsHelper
{
    public class DragDropHelper
    {
        #region DragCommand
        public static ICommand GetDragCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(DragCommandProperty);
        }

        public static void SetDragCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(DragCommandProperty, value);
        }

        public static readonly DependencyProperty DragCommandProperty =
            DependencyProperty.RegisterAttached("DragCommand",
            typeof(ICommand),
            typeof(DragDropHelper),
            new PropertyMetadata(null,
            new PropertyChangedCallback(SetupDrag)));
        #endregion

        #region DragCommandParameter
        public static object GetDragCommandParameter(DependencyObject obj)
        {
            return (object)obj.GetValue(DragCommandParameterProperty);
        }

        public static void SetDragCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(DragCommandParameterProperty, value);
        }

        public static readonly DependencyProperty DragCommandParameterProperty =
            DependencyProperty.RegisterAttached("DragCommandParameter",
            typeof(object),
            typeof(DragDropHelper),
            new PropertyMetadata(null));
        #endregion

        #region DropCommand
        public static ICommand GetDropCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(DropCommandProperty);
        }

        public static void SetDropCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(DropCommandProperty, value);
        }

        public static readonly DependencyProperty DropCommandProperty =
            DependencyProperty.RegisterAttached("DropCommand",
            typeof(ICommand),
            typeof(DragDropHelper),
            new PropertyMetadata(null,
            new PropertyChangedCallback(SetupDrop)));
        #endregion

        #region DropCommandParameter
        public static object GetDropCommandParameter(DependencyObject obj)
        {
            return (object)obj.GetValue(DropCommandParameterProperty);
        }

        public static void SetDropCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(DropCommandParameterProperty, value);
        }

        public static readonly DependencyProperty DropCommandParameterProperty =
            DependencyProperty.RegisterAttached("DropCommandParameter",
            typeof(object),
            typeof(DragDropHelper),
            new PropertyMetadata(null)); 
        #endregion

        public static DragIcon GetDataPackageOperation(DependencyObject obj)
        {
            return (DragIcon)obj.GetValue(DataPackageOperationProperty);
        }

        public static void SetDataPackageOperation(DependencyObject obj, DragIcon value)
        {
            obj.SetValue(DataPackageOperationProperty, value);
        }

        public static readonly DependencyProperty DataPackageOperationProperty =
            DependencyProperty.RegisterAttached("DataPackageOperation",
            typeof(DragIcon),
            typeof(DragDropHelper),
            new PropertyMetadata(DragIcon.Copy));

        private static void SetupDrag(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            UIElement ctl = obj as UIElement;

            if (ctl != null)
            {
                ICommand oldValue = (ICommand)e.OldValue;
                ICommand newValue = (ICommand)e.NewValue;

                if (oldValue == null && newValue != null)
                {
                    ctl.CanDrag = true;
                    ctl.DragStarting += Ctl_DragStarting;
                }
                else if (oldValue != null && newValue == null)
                {
                    ctl.CanDrag = false;
                    ctl.DragStarting -= Ctl_DragStarting;
                }
            }
        }

        private static void Ctl_DragStarting(UIElement sender, DragStartingEventArgs args)
        {
            var element = sender as UIElement;
            var command = GetDragCommand(element);
            var parameter = GetDragCommandParameter(element);

            if (command != null)
            {
                if (command.CanExecute(parameter))
                {
                    command.Execute(parameter);
                }
            }
        }

        private static void SetupDrop(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            UIElement ctl = obj as UIElement;

            if (ctl != null)
            {
                ICommand oldValue = (ICommand)e.OldValue;
                ICommand newValue = (ICommand)e.NewValue;

                if (oldValue == null && newValue != null)
                {
                    ctl.AllowDrop = true;
                    ctl.DragOver += Ctl_DragOver;
                    ctl.Drop += Ctl_Drop;
                }
                else if (oldValue != null && newValue == null)
                {
                    ctl.AllowDrop = false;
                    ctl.DragOver -= Ctl_DragOver;
                    ctl.Drop -= Ctl_Drop;
                }
            }
        }

        private static void Ctl_Drop(object sender, DragEventArgs e)
        {
            var element = sender as UIElement;
            var command = GetDropCommand(element);
            object parameter = GetDropCommandParameter(element);

            if (command != null)
            {
                if (command.CanExecute(parameter))
                {
                    command.Execute(parameter);
                }
            }
        }

        private static void Ctl_DragOver(object sender, DragEventArgs e)
        {
            var element = sender as UIElement;
            var command = GetDropCommand(element);
            object parameter = GetDropCommandParameter(element);
            bool can = command.CanExecute(parameter);

            if (can)
            {
                var operation = GetDataPackageOperation(element);
                e.AcceptedOperation = (DataPackageOperation)
                    Enum.Parse(typeof(DataPackageOperation), operation.ToString());
            }
            else
            {
                e.AcceptedOperation = DataPackageOperation.None;
            }
        }
    }
}