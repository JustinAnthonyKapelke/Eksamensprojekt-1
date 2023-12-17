﻿using Nordlangelands_Tækkemand.ViewModel;
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
            //this.Closing += CreateMaterialWindow_Closing;
        }

        //Replace the close window event with hide method
        //private void CreateMaterialWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    // Cancel the closing of the window
        //    e.Cancel = true;

        //    // Hide the window
        //    this.Hide();
        //}
    }
}
