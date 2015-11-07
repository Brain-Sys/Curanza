using System;
using System.Windows.Input;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace BrainSys.UWP.Curanza.CommandsHelper
{
    public class FrameworkElementHelper
    {
        #region LoadedCommand
        public static ICommand GetLoadedCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(LoadedCommandProperty);
        }

        public static void SetLoadedCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(LoadedCommandProperty, value);
        }

        public static readonly DependencyProperty LoadedCommandProperty =
            DependencyProperty.RegisterAttached("LoadedCommand",
            typeof(ICommand),
            typeof(FrameworkElementHelper),
            new PropertyMetadata(null,
            new PropertyChangedCallback(SetupLoaded)));
        #endregion

        #region LoadedCommandParameter
        public static object GetLoadedCommandParameter(DependencyObject obj)
        {
            return (object)obj.GetValue(LoadedCommandParameterProperty);
        }

        public static void SetLoadedCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(LoadedCommandParameterProperty, value);
        }

        public static readonly DependencyProperty LoadedCommandParameterProperty =
            DependencyProperty.RegisterAttached("LoadedCommandParameter",
            typeof(object),
            typeof(FrameworkElementHelper),
            new PropertyMetadata(null));
        #endregion

        #region Loaded
        private static void SetupLoaded(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var ctl = obj as FrameworkElement;

            if (ctl != null)
            {
                ICommand oldValue = (ICommand)e.OldValue;
                ICommand newValue = (ICommand)e.NewValue;

                if (oldValue == null && newValue != null)
                {
                    ctl.Loaded += Ctl_Loaded;
                }
                else if (oldValue != null && newValue == null)
                {
                    ctl.Loaded -= Ctl_Loaded;
                }
            }
        }

        private static void Ctl_Loaded(object sender, RoutedEventArgs e)
        {
            var element = sender as FrameworkElement;
            var command = GetLoadedCommand(element);
            var parameter = GetLoadedCommandParameter(element);

            if (command != null)
            {
                if (command.CanExecute(parameter))
                {
                    command.Execute(parameter);
                }
            }
        }
        #endregion

        #region DoubleTappedCommand
        public static ICommand GetDoubleTappedCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(DoubleTappedProperty);
        }

        public static void SetDoubleTappedCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(DoubleTappedProperty, value);
        }

        public static readonly DependencyProperty DoubleTappedProperty =
            DependencyProperty.RegisterAttached("DoubleTappedCommand",
            typeof(ICommand),
            typeof(FrameworkElementHelper),
            new PropertyMetadata(null,
            new PropertyChangedCallback(SetupDoubleTapped)));
        #endregion

        #region DoubleTappedCommandParameter
        public static object GetDoubleTappedCommandParameter(DependencyObject obj)
        {
            return (object)obj.GetValue(DoubleTappedCommandParameterProperty);
        }

        public static void SetDoubleTappedCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(DoubleTappedCommandParameterProperty, value);
        }

        public static readonly DependencyProperty DoubleTappedCommandParameterProperty =
            DependencyProperty.RegisterAttached("DoubleTappedCommandParameter",
            typeof(object),
            typeof(FrameworkElementHelper),
            new PropertyMetadata(null));
        #endregion

        #region DoubleTapped
        private static void SetupDoubleTapped(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctl = d as FrameworkElement;

            if (ctl != null)
            {
                ICommand oldValue = (ICommand)e.OldValue;
                ICommand newValue = (ICommand)e.NewValue;

                if (oldValue == null && newValue != null)
                {
                    ctl.IsDoubleTapEnabled = true;
                    ctl.DoubleTapped += Ctl_DoubleTapped;
                }
                else if (oldValue != null && newValue == null)
                {
                    ctl.DoubleTapped -= Ctl_DoubleTapped;
                }
            }
        }

        private static void Ctl_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            var element = sender as FrameworkElement;
            var command = GetDoubleTappedCommand(element);
            var parameter = GetDoubleTappedCommandParameter(element);

            if (command != null)
            {
                if (command.CanExecute(parameter))
                {
                    command.Execute(parameter);
                }
            }
        }
        #endregion

        #region KeyDownCommand
        public static ICommand GetKeyDownCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(KeyDownCommandProperty);
        }

        public static void SetKeyDownCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(KeyDownCommandProperty, value);
        }

        public static readonly DependencyProperty KeyDownCommandProperty =
            DependencyProperty.RegisterAttached("KeyDownCommand",
            typeof(ICommand),
            typeof(FrameworkElementHelper),
            new PropertyMetadata(null,
            new PropertyChangedCallback(SetupKeyDownCommand)));
        #endregion

        #region KeyDownCommandParameter
        public static object GetKeyDownCommandParameter(DependencyObject obj)
        {
            return obj.GetValue(KeyDownCommandParameterProperty);
        }

        public static void SetKeyDownCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(KeyDownCommandParameterProperty, value);
        }

        public static readonly DependencyProperty KeyDownCommandParameterProperty =
                    DependencyProperty.RegisterAttached("KeyDownCommandParameter",
                    typeof(object),
                    typeof(FrameworkElementHelper),
                    new PropertyMetadata(null));
        #endregion

        #region Key
        public static VirtualKey GetKey(DependencyObject obj)
        {
            return (VirtualKey)obj.GetValue(KeyProperty);
        }

        public static void SetKey(DependencyObject obj, VirtualKey value)
        {
            obj.SetValue(KeyProperty, value);
        }

        public static readonly DependencyProperty KeyProperty =
                    DependencyProperty.RegisterAttached("Key",
                    typeof(VirtualKey),
                    typeof(FrameworkElementHelper),
                    new PropertyMetadata(VirtualKey.Enter));
        #endregion

        #region KeyDown management
        private static void SetupKeyDownCommand(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var ctl = obj as FrameworkElement;

            if (ctl != null)
            {
                ICommand oldValue = (ICommand)e.OldValue;
                ICommand newValue = (ICommand)e.NewValue;

                if (oldValue == null && newValue != null)
                {
                    {
                        ctl.KeyDown += Ctl_KeyDown;
                    }
                }
                else if (oldValue != null && newValue == null)
                {
                    ctl.KeyDown -= Ctl_KeyDown;
                }
            }
        }

        private static void Ctl_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            e.Handled = true;
            var element = sender as DependencyObject;

            if (e.Key == GetKey(element))
            {
                var command = GetKeyDownCommand(element);
                var parameter = element.GetValue(KeyDownCommandParameterProperty);

                if (command != null)
                {
                    if (command.CanExecute(parameter))
                    {
                        command.Execute(parameter);
                    }
                }
            }
        } 
        #endregion
    }
}