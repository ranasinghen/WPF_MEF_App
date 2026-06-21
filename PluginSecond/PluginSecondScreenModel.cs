using InternalShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginSecond
{
    public class PluginSecondScreenModel : NotifyModelBase
    {
        private double ValueVar;
        public double Value
        {
            get { return ValueVar; }
            set
            {
                ValueVar = value;				
                NotifyChangedThis();
            }
        }
		

        public PluginSecondScreenModel()
        {
        }
    }
}
