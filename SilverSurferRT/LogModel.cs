using SilverSurferLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace SilverSurfer
{
    public class LogModel
    {
        public string Info { get; set; }
        public SolidColorBrush InfoColor { get; set; }

        public SilverExpression Expression { get; set; }
        public string RawExpression { get; set; }
        public double? Result { get; set; }
    }
}
