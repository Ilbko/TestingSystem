using System;
using System.Globalization;
using System.Windows.Data;

namespace WPF_Exam.ViewModel
{
    public class AdminRadioConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //if ((int)value == int.Parse(parameter.ToString()))
            //    return true;

            //return false;
            return (int)value == int.Parse(parameter.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }
}
