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
        }


        //public void GetTypeFromRadioButton(MainViewModel mvm)
        //{
        //    // Ensure that SelectedMaterial is not null before trying to access its properties
        //    if (mvm.SelectedMaterial != null)
        //    {
        //        //var selectedMaterialType = mvm.SelectedMaterial.MaterialType;

        //        // Update the ViewModel property instead of directly setting the RadioButton
        //        if(mvm.CurrentVM == mvm.ThatchingVM)              
        //        ThatchingTypeRadioButton.IsChecked = true;
        //    }
        //    else
        //    {
        //        // Reset the ViewModel property if SelectedMaterial is null
        //        ThatchingTypeRadioButton.IsChecked = false;
        //    }
        //}

        //mvm.CurrentVM

        //if(mvm.CurrentVM = mvm. ThatchingVM)
        //  {
        //      isThatchingChecked == true;
        //  }

        //bool IsThatchingChecked
       




    }
}
    
