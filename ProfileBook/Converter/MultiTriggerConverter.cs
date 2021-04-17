using System;
using System.Globalization;
using Xamarin.Forms;

namespace ProfileBook.Converter
{
    class MultiTriggerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value > 0) // length > 0 ?
                // some data has been entered
                return true;
            else
                // input is empty
                return false;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
