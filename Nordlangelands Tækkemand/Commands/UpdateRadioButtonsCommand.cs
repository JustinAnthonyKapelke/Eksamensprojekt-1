using Nordlangelands_Tækkemand.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Nordlangelands_Tækkemand.Commands
{
    internal class UpdateRadioButtonsCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        //{
        //    add { CommandManager.RequerySuggested += value; }
        //     remove { CommandManager.RequerySuggested -= value; }
        //}

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is MainViewModel mvm)
            {
                //Tilgå CreateMaterialWindow igennem MainViewModelinstansen mvm
                var createMaterialWindow = mvm.MainWindowInstance.UpdateMaterialWindow;

                if (mvm.SelectedMaterial != null)
                {
                    var SelectedMaterialTypeID = mvm.SelectedMaterial.MaterialTypeID;


                    // Update the ViewModel property instead of directly setting the RadioButton
                    //if (SelectedMaterialType == "Tække")
                    //    mvm.MainWindowInstance.UpdateMaterialWindow.ThatchingTypeRadioButton.IsChecked = true;

                    switch (SelectedMaterialTypeID)
                    {
                        case 1:
                            mvm.MainWindowInstance.UpdateMaterialWindow.ThatchingTypeRadioButton.IsChecked = true;
                            break;
                        case 2:
                            mvm.MainWindowInstance.UpdateMaterialWindow.WoodTypeRadioButton.IsChecked = true;
                            break;
                        case 3:
                            mvm.MainWindowInstance.UpdateMaterialWindow.VariousTypeRadioButton.IsChecked = true;
                            break;
                    }
                }

                if (mvm.SelectedMaterial != null)
                {
                    if (mvm.MainWindowInstance.UpdateMaterialWindow.ThatchingTypeRadioButton.IsChecked == true)
                    {
                        mvm.SelectedMaterial.MaterialTypeID = 1;
                    }

                    if (mvm.MainWindowInstance.UpdateMaterialWindow.WoodTypeRadioButton.IsChecked == true)
                    {
                        mvm.SelectedMaterial.MaterialTypeID = 2;
                    }

                    if (mvm.MainWindowInstance.UpdateMaterialWindow.VariousTypeRadioButton.IsChecked == true)
                    {
                        mvm.SelectedMaterial.MaterialTypeID = 3;
                    }
                }


            }
        }
    }
}
