using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace BrainSys.UWP.Curanza.CommandsHelper
{
    public class TextBoxHelper
    {
        #region AutoSelectAll
        public static bool GetAutoSelectAll(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoSelectAllProperty);
        }

        public static void SetAutoSelectAll(DependencyObject obj,
            bool value)
        {
            obj.SetValue(AutoSelectAllProperty, value);
        }

        public static readonly DependencyProperty AutoSelectAllProperty =
            DependencyProperty.RegisterAttached("AutoSelectAll",
            typeof(bool),
            typeof(TextBoxHelper),
            new PropertyMetadata(false,
            new PropertyChangedCallback(SetupFocus)));

        private static void SetupFocus(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            TextBox tb = obj as TextBox;

            if (tb != null)
            {
                bool oldValue = (bool)e.OldValue;
                bool newValue = (bool)e.NewValue;

                if (oldValue == false && newValue == true)
                {
                    tb.GotFocus += (s2, e2) =>
                    {
                        TextBox tb2 = s2 as TextBox;

                        if (tb != null)
                        {
                            tb.SelectAll();
                        }
                    };
                }
            }
        }
        #endregion

        #region Regex Command
        public static ICommand GetRegexCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(RegexCommandProperty);
        }

        public static void SetRegexCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(RegexCommandProperty, value);
        }

        public static readonly DependencyProperty RegexCommandProperty =
            DependencyProperty.RegisterAttached("RegexCommand",
            typeof(ICommand),
            typeof(TextBoxHelper),
            new PropertyMetadata(null,
            new PropertyChangedCallback(Setup)));

        public static object GetRegexCommandParameter(DependencyObject obj)
        {
            return (object)obj.GetValue(RegexCommandParameterProperty);
        }

        public static void SetRegexCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(RegexCommandParameterProperty, value);
        }

        public static readonly DependencyProperty RegexCommandParameterProperty =
            DependencyProperty.RegisterAttached("RegexCommandParameter",
            typeof(object),
            typeof(TextBoxHelper),
            new PropertyMetadata(null));

        public static string GetRegexExpression(DependencyObject obj)
        {
            return (string)obj.GetValue(RegexExpressionProperty);
        }

        public static void SetRegexExpression(DependencyObject obj, string value)
        {
            obj.SetValue(RegexExpressionProperty, value);
        }

        public static readonly DependencyProperty RegexExpressionProperty =
            DependencyProperty.RegisterAttached("RegexExpression",
            typeof(string),
            typeof(TextBoxHelper),
            new PropertyMetadata(null));

        private static void Setup(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var ctl = obj as TextBox;

            if (ctl != null)
            {
                ICommand oldValue = (ICommand)e.OldValue;
                ICommand newValue = (ICommand)e.NewValue;

                if (oldValue == null && newValue != null)
                {
                    ctl.TextChanging += Ctl_TextChanging;
                }
            }
        }

        private static void Ctl_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            string expression = GetRegexExpression(sender);
            if (string.IsNullOrEmpty(expression)) return;

            Regex r = new Regex(expression);
            bool result = r.IsMatch(sender.Text);

            if (result)
            {
                var command = GetRegexCommand(sender);
                var parameter = GetRegexCommandParameter(sender);

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