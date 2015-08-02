using System;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace BrainSys.UWP.Curanza.CommandsHelper
{
    public class Swipe
    {
        #region Up property
        public static ICommand GetUpCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(UpCommandProperty);
        }

        public static void SetUpCommand(DependencyObject obj,
            ICommand value)
        {
            obj.SetValue(UpCommandProperty, value);
        }

        public static readonly DependencyProperty UpCommandProperty =
            DependencyProperty.RegisterAttached("UpCommand",
            typeof(ICommand),
            typeof(Swipe),
            new PropertyMetadata(null,
            new PropertyChangedCallback(Setup)));

        public static object GetUpCommandParameter(DependencyObject obj)
        {
            return (object)obj.GetValue(UpCommandParameterProperty);
        }

        public static void SetUpCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(UpCommandParameterProperty, value);
        }

        public static readonly DependencyProperty UpCommandParameterProperty =
            DependencyProperty.RegisterAttached("UpCommandParameter",
            typeof(object),
            typeof(Swipe),
            new PropertyMetadata(null));
        #endregion

        #region Down property
        public static ICommand GetDownCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(DownCommandProperty);
        }

        public static void SetDownCommand(DependencyObject obj,
            ICommand value)
        {
            obj.SetValue(DownCommandProperty, value);
        }

        public static readonly DependencyProperty DownCommandProperty =
            DependencyProperty.RegisterAttached("DownCommand",
            typeof(ICommand),
            typeof(Swipe),
            new PropertyMetadata(null,
            new PropertyChangedCallback(Setup)));

        public static object GetDownCommandParameter(DependencyObject obj)
        {
            return (object)obj.GetValue(DownCommandParameterProperty);
        }

        public static void SetDownCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(DownCommandParameterProperty, value);
        }

        public static readonly DependencyProperty DownCommandParameterProperty =
            DependencyProperty.RegisterAttached("DownCommandParameter",
            typeof(object),
            typeof(Swipe),
            new PropertyMetadata(null));
        #endregion

        #region Right property
        public static ICommand GetRightCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(RightCommandProperty);
        }

        public static void SetRightCommand(DependencyObject obj,
            ICommand value)
        {
            obj.SetValue(RightCommandProperty, value);
        }

        public static readonly DependencyProperty RightCommandProperty =
            DependencyProperty.RegisterAttached("RightCommand",
            typeof(ICommand),
            typeof(Swipe),
            new PropertyMetadata(null,
            new PropertyChangedCallback(Setup)));

        public static object GetRightCommandParameter(DependencyObject obj)
        {
            return (object)obj.GetValue(RightCommandParameterProperty);
        }

        public static void SetRightCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(RightCommandParameterProperty, value);
        }

        public static readonly DependencyProperty RightCommandParameterProperty =
            DependencyProperty.RegisterAttached("RightCommandParameter",
            typeof(object),
            typeof(Swipe),
            new PropertyMetadata(null));
        #endregion

        #region Left property
        public static ICommand GetLeftCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(LeftCommandProperty);
        }

        public static void SetLeftCommand(DependencyObject obj,
            ICommand value)
        {
            obj.SetValue(LeftCommandProperty, value);
        }

        public static readonly DependencyProperty LeftCommandProperty =
            DependencyProperty.RegisterAttached("LeftCommand",
            typeof(ICommand),
            typeof(Swipe),
            new PropertyMetadata(null,
            new PropertyChangedCallback(Setup)));

        public static object GetLeftCommandParameter(DependencyObject obj)
        {
            return (object)obj.GetValue(LeftCommandParameterProperty);
        }

        public static void SetLeftCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(LeftCommandParameterProperty, value);
        }

        public static readonly DependencyProperty LeftCommandParameterProperty =
            DependencyProperty.RegisterAttached("LeftCommandParameter",
            typeof(object),
            typeof(Swipe),
            new PropertyMetadata(null));
        #endregion

        #region Attached (to avoid to subscribe the event 'ManipulationCompleted' multiple times
        public static bool GetAttached(DependencyObject obj)
        {
            return (bool)obj.GetValue(AttachedProperty);
        }

        public static void SetAttached(DependencyObject obj,
            bool value)
        {
            obj.SetValue(AttachedProperty, value);
        }

        private static readonly DependencyProperty AttachedProperty =
            DependencyProperty.RegisterAttached("Attached",
            typeof(bool),
            typeof(UIElement),
            new PropertyMetadata(false));
        #endregion

        private static void Setup(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var ctl = obj as UIElement;

            if (ctl != null)
            {
                ICommand oldValue = (ICommand)e.OldValue;
                ICommand newValue = (ICommand)e.NewValue;

                if (oldValue == null && newValue != null)
                {
                    {
                        if (GetAttached(ctl) == false)
                        {
                            SetAttached(ctl, true);
                            ctl.ManipulationCompleted += ctl_ManipulationCompleted;
                        }
                    }
                }
                else if (oldValue != null && newValue == null)
                {
                    ctl.SetValue(AttachedProperty, false);
                    ctl.ManipulationCompleted -= ctl_ManipulationCompleted;
                }
            }
        }

        static void ctl_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            var element = sender as UIElement;
            ICommand command = null;
            object parameter = null;

            // x < 0 --> verso sinistra
            // x > 0 --> verso destra
            var x = e.Cumulative.Translation.X;

            // y < 0 --> verso l'alto
            // y > 0 --> verso il basso
            var y = e.Cumulative.Translation.Y;

            if (y < 0 && Math.Abs(y) > Math.Abs(x))
            {
                command = GetDownCommand(element);
                parameter = GetDownCommandParameter(element);
            }
            else if (y > 0 && Math.Abs(y) > Math.Abs(x))
            {
                command = GetUpCommand(element);
                parameter = GetUpCommandParameter(element);
            }
            else if (x > 0 && Math.Abs(x) > Math.Abs(y))
            {
                command = GetLeftCommand(element);
                parameter = GetLeftCommandParameter(element);
            }
            else if (x < 0 && Math.Abs(x) > Math.Abs(y))
            {
                command = GetRightCommand(element);
                parameter = GetRightCommandParameter(element);
            }

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