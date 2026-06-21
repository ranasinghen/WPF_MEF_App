using InternalShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginCalculator
{
    public class CalculatorScreenModel : NotifyModelBase
    {
        public CalculatorScreenModel()
        {
            _calculateCommand = new DelegateCommand(CalculateSUM);
        }

        private void CalculateSUM()
        {
            Sum = NumberOne + NumberTwo;
        }

        private double NumberOneVar;
        public double NumberOne
        {
            get { return NumberOneVar; }
            set
            {
                NumberOneVar = value;                
            }
        }
		
        private double NumberTwoVar;
        public double NumberTwo
        {
            get { return NumberTwoVar; }
            set
            {
                NumberTwoVar = value;                
            }
        }
		
        private double SumVar;
        public double Sum
        {
            get { return SumVar; }
            set
            {
                SumVar = value;				
                NotifyChangedThis();
            }
        }

        private DelegateCommand _calculateCommand;

        public DelegateCommand CalculateCommand
        {
            get { return _calculateCommand; }
            set { _calculateCommand = value; }
        }

    }
}
