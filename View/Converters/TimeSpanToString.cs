using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Project_DeRidderJonas_HypixelApi.View.Converters
{
    class TimeSpanToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan? timespan = value as TimeSpan?;
            if (timespan == null || timespan?.TotalSeconds <= 0) return "Not available";

            StringBuilder stringBuilder = new StringBuilder();
            if (timespan?.Days > 0) stringBuilder.Append($"{timespan?.Days}d");
            if (timespan?.Hours > 0) stringBuilder.Append($" {timespan?.Hours}h");
            if (timespan?.Minutes > 0) stringBuilder.Append($" {timespan?.Minutes}m");
            return stringBuilder.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
