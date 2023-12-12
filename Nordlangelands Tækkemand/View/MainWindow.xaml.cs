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
        MainViewModel mvm;
        public CreateMaterialWindow CreateMaterialWindow;
        public UpdateMaterialWindow UpdateMaterialWindow;
        public CreateWorkplaceWindow CreateWorkplaceWindow;

        public MainWindow()
        {
            //Instansier MainVievModel med den aktuelle instans af MainWindow
            mvm = new MainViewModel(this);
            InitializeComponent();
            // Indstil DataContext for MainWindow til MainViewModel
            DataContext = mvm;
            CreateMaterialWindow = new CreateMaterialWindow(mvm);
            UpdateMaterialWindow = new UpdateMaterialWindow(mvm);
            CreateWorkplaceWindow = new CreateWorkplaceWindow(mvm);
            mvm.LogTextCMD.Execute(mvm);
        }

        private void SearchMaterialTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            mvm.SearchMaterialCMD.Execute(mvm);
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "Søg materiale")
            {
                textBox.Text = string.Empty;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Søg materiale";
            }
        }          
    }
}
