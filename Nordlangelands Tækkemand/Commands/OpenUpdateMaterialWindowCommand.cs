using Nordlangelands_Tækkemand.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Nordlangelands_Tækkemand.Commands
{
    internal class OpenUpdateMaterialWindowCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return parameter is MainViewModel mvm && mvm.SelectedMaterial != null;
        }

        public void Execute(object? parameter)
        {
            if(parameter is MainViewModel mvm)
            {
                mvm.MainWindowInstance.UpdateMaterialWindow.Show();


                //Update radiobuttons based on the selected material
                if (mvm.SelectedMaterial != null)
                {
                    var SelectedMaterialType = mvm.SelectedMaterial.MaterialType;


                    // Update the ViewModel property instead of directly setting the RadioButton
                    //if (SelectedMaterialType == "Tække")
                    //    mvm.MainWindowInstance.UpdateMaterialWindow.ThatchingTypeRadioButton.IsChecked = true;

                    switch (SelectedMaterialType)
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
            }
        }
    }
}
