using SilverSurferLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SilverSurfer
{
    class ViewModel
    {
        public ViewModel()
        {
            History = new ObservableCollection<LogModel>();
            SavedExpressions = new ObservableCollection<SilverExpression>();
        }
        public ObservableCollection<SilverExpression> SavedExpressions { get; set; }
        public ObservableCollection<LogModel> History { get; set; }
    }
}
