using SilverSurferLib;
using SilverSurferLib.Tokens;
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
            SavedExpressions = new ObservableCollection<Token>();
        }
        public ObservableCollection<Token> SavedExpressions { get; set; }
        public ObservableCollection<LogModel> History { get; set; }
    }
}
