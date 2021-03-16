using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Project_DeRidderJonas_HypixelApi.View.Converters
{
    class BoolToOnlineImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isOnline = (bool)value;

            if (!DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                if (isOnline) return new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Online.png", UriKind.Absolute));
                else return new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Offline.png", UriKind.Absolute));
            }

            return new BitmapImage();

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
