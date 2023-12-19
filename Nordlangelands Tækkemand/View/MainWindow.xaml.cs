using Nordlangelands_Tækkemand.Commands;
using Nordlangelands_Tækkemand.View;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Nordlangelands_Tækkemand
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Field
        MainViewModel MVM = new MainViewModel();

        //Constructor
        public MainWindow()
        {          
            InitializeComponent();
            DataContext = MVM;  
            MVM.LogTextCMD.Execute(MVM);
        }

        //Execute SeacrMaterialCommand everytime the text in SearchMaterialTextBox is changed
        private void SearchMaterialTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            MVM.SearchMaterialCMD.Execute(MVM);
        }
   
        //Close the application
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

                
    }
}
