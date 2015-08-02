using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace BrainSys.UWP.Curanza.CommandsHelper
{
    public class ListViewBaseHelper
    {
        public static ICommand GetItemClickCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(ItemClickCommandProperty);
        }

        public static void SetItemClickCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(ItemClickCommandProperty, value);
        }

        public static readonly DependencyProperty ItemClickCommandProperty =
            DependencyProperty.RegisterAttached("ItemClickCommand",
            typeof(ICommand),
            typeof(ListViewBaseHelper),
            new PropertyMetadata(null,
            new PropertyChangedCallback(Setup)));

        private static void Setup(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var ctl = obj as ListViewBase;

            if (ctl != null)
            {
                ICommand oldValue = (ICommand)e.OldValue;
                ICommand newValue = (ICommand)e.NewValue;

                if (oldValue == null && newValue != null)
                {
                    ctl.IsItemClickEnabled = true;
                    ctl.ItemClick += Ctl_ItemClick;
                }
                else if (oldValue != null && newValue == null)
                {
                    ctl.IsItemClickEnabled = false;
                    ctl.ItemClick -= Ctl_ItemClick;
                }
            }
        }

        private static void Ctl_ItemClick(object sender, ItemClickEventArgs e)
        {
            var element = sender as UIElement;
            var command = GetItemClickCommand(element);

            if (command != null)
            {
                if (command.CanExecute(e.ClickedItem))
                {
                    command.Execute(e.ClickedItem);
                }
            }
        }
    }
}