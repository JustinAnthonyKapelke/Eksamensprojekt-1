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

namespace Nordlangelands_Tækkemand.View
{
    /// <summary>
    /// Interaction logic for UpdateMaterialWindow.xaml
    /// </summary>
    public partial class UpdateMaterialWindow : Window
    {

        public UpdateMaterialWindow(MainViewModel mvm)
        {
            InitializeComponent();
            DataContext = mvm;           
            this.Closing += CreateMaterialWindow_Closing;
        }


        private void CreateMaterialWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Cancel the closing of the window
            e.Cancel = true;

            // Hide the window
            this.Hide();
        }
    }
}
    
