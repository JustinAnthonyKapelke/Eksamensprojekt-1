using Nordlangelands_Tækkemand.ViewModel;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Nordlangelands_Tækkemand
{   
    public partial class WorkplaceWindow : Window
    {
   
        public WorkplaceWindow(MainViewModel mvm)
        {
            InitializeComponent();            
            DataContext = mvm;            
        }
    }
}
