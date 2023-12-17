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
        MainViewModel mvm = new MainViewModel();


        public MainWindow()
        {          
            InitializeComponent();
            // Set DataContext for MainWindow to MainViewModel mvm
            DataContext = mvm;  
            mvm.LogTextCMD.Execute(mvm);
        }

        //Execute the command everytime the text is changed
        private void SearchMaterialTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            mvm.SearchMaterialCMD.Execute(mvm);
        }
   
        //Close the program
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

                
    }
}
