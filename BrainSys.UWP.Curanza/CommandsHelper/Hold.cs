using System.Windows.Input;
using Windows.UI.Xaml;

namespace BrainSys.UWP.Curanza.CommandsHelper
{
    public class Hold
    {
        public static ICommand GetCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandProperty);
        }

        public static void SetCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandProperty, value);
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command",
            typeof(ICommand),
            typeof(Hold),
            new PropertyMetadata(null,
            new PropertyChangedCallback(Setup)));

        public static object GetCommandParameter(DependencyObject obj)
        {
            return (object)obj.GetValue(CommandParameterProperty);
        }

        public static void SetCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(CommandParameterProperty, value);
        }

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached("CommandParameter",
            typeof(object),
            typeof(Hold),
            new PropertyMetadata(null));

        private static void Setup(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            UIElement ctl = obj as UIElement;

            if (ctl != null)
            {
                ICommand oldValue = (ICommand)e.OldValue;
                ICommand newValue = (ICommand)e.NewValue;

                if (oldValue == null && newValue != null)
                {
                    ctl.Holding += ctl_Holding;
                }
                else if (oldValue != null && newValue == null)
                {
                    ctl.Holding -= ctl_Holding;
                }
            }
        }

        static void ctl_Holding(object sender, Windows.UI.Xaml.Input.HoldingRoutedEventArgs e)
        {
            var element = sender as UIElement;
            var command = GetCommand(element);
            var parameter = GetCommandParameter(element);

            if (command != null)
            {
                if (command.CanExecute(parameter))
                {
                    command.Execute(parameter);
                }
            }
        }
    }
}