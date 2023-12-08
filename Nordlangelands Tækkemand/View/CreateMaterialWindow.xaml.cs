using Nordlangelands_Tækkemand.ViewModel;
using Nordlangelands_Tækkemand.Commands;
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
   //<summary>
   // Interaction logic for CreateMaterialWindow.xaml
   //  </summary>
    public partial class CreateMaterialWindow : Window
    {      

        public CreateMaterialWindow(MainViewModel mvm)
        {
            InitializeComponent();
            DataContext = mvm;
        }    
    }
}
