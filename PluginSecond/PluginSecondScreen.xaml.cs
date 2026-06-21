using InternalShared;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PluginSecond
{
    /// <summary>
    /// Interaction logic for PluginSecondScreen.xaml
    /// </summary>
    [Export(typeof(IView)), PartCreationPolicy(CreationPolicy.Any)]
    [ExportMetadata("Name", "PluginSecond")]
    public partial class PluginSecondScreen : UserControl, IView
    {
        public PluginSecondScreen()
        {
            InitializeComponent();
        }
    }
}
