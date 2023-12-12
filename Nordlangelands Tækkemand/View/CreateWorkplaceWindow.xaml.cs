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
    /// <summary>
    /// Interaction logic for CreateWorkplaceWindow.xaml
    /// </summary>
    public partial class CreateWorkplaceWindow : Window
    {
        public CreateWorkplaceWindow(MainViewModel mvm)
        {
            InitializeComponent();
            DataContext = mvm;
            this.Closing += CreateWorkplaceWindow_Closing;
        }
        private void CreateWorkplaceWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Cancel the closing of the window
            e.Cancel = true;

            // Hide the window
            this.Hide();
        }
    }
}
