using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace SilverSurfer.Common
{
    public class KeyValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            IDictionary<string, double> dict = (IDictionary<string, double>)value;
            return dict.Select(a => Tuple.Create(a.Key, a.Value)).ToList();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            string[] parts = value.ToString().Split('\t');
            return new KeyValuePair<string,double>(parts[0], double.Parse(parts[1]));
        }
    }
}
