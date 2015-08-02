using System;
using BrainSys.UWP.Curanza.SampleApp.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace BrainSys.UWP.Curanza.SampleApp.Helpers
{
    public class FeatureVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null) return null;
            if (parameter == null) return null;

            string name = (value as Feature).Id;
            string expected = parameter.ToString();

            if (name == expected)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}