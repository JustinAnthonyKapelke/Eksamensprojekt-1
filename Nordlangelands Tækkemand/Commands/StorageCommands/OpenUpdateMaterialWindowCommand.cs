using Nordlangelands_Tækkemand.View;
using Nordlangelands_Tækkemand.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Nordlangelands_Tækkemand.Commands.StorageCommands
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
            if (parameter is MainViewModel mvm)
            {
                UpdateMaterialWindow updateMaterialWindow = new(mvm);
                updateMaterialWindow.Show();


                //Update radiobuttons based on the selected material
                if (mvm.SelectedMaterial != null)
                {
                    var SelectedMaterialType = mvm.SelectedMaterial.MaterialType;
                    switch (SelectedMaterialType)
                    {
                        case "Tække":
                            mvm.UpdateIsThatchingChecked = true;
                            break;
                        case "Træ":
                            mvm.UpdateIsWoodChecked= true;
                            break;
                        case "Diverse":
                            mvm.UpdateIsVariousChecked = true;
                            break;
                    }
                }
            }
        }
    }
}
