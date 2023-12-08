using Nordlangelands_Tækkemand.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Nordlangelands_Tækkemand.Commands
{
    internal class UpdateRadioButtons : ICommand
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
                    var SelectedMaterialType = mvm.SelectedMaterial.MaterialType;
                    

                    // Update the ViewModel property instead of directly setting the RadioButton
                    //if (SelectedMaterialType == "Tække")
                    //    mvm.MainWindowInstance.UpdateMaterialWindow.ThatchingTypeRadioButton.IsChecked = true;

                    switch(SelectedMaterialType)
                    {
                        case "Tække":
                            mvm.MainWindowInstance.UpdateMaterialWindow.ThatchingTypeRadioButton.IsChecked = true;
                            break;
                        case "Træ":
                            mvm.MainWindowInstance.UpdateMaterialWindow.WoodTypeRadioButton.IsChecked = true;
                            break;
                        case "Diverse":
                            mvm.MainWindowInstance.UpdateMaterialWindow.VariousTypeRadioButton.IsChecked = true;
                            break;
                    }
                }
                else
                {
                    // Reset the ViewModel property if SelectedMaterial is null
                    mvm.MainWindowInstance.UpdateMaterialWindow.ThatchingTypeRadioButton.IsChecked = false;
                    mvm.MainWindowInstance.UpdateMaterialWindow.WoodTypeRadioButton.IsChecked = false;
                    mvm.MainWindowInstance.UpdateMaterialWindow.VariousTypeRadioButton.IsChecked = false;
                }

            }
        }
    }
}
