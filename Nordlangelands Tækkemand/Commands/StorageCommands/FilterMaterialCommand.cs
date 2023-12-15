using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Nordlangelands_Tækkemand.ViewModel;

namespace Nordlangelands_Tækkemand.Commands.StorageCommands
{
    public class FilterMaterialCommand : ICommand
    {

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is MainViewModel mvm)
            {
                bool? isAllChecked = mvm.MainWindowInstance.AllMaterials_RadioButton.IsChecked;
                bool? isThatchingChecked = mvm.MainWindowInstance.Thatching_RadioButton.IsChecked;
                bool? isWoodChecked = mvm.MainWindowInstance.Wood_RadioButton.IsChecked;
                bool? isVariousChecked = mvm.MainWindowInstance.Various_RadioButton.IsChecked;

                if (isAllChecked == true)
                {
                    mvm.CurrentVM = mvm.AllMaterialsVM;
                }

                if (isThatchingChecked == true)
                {
                    mvm.CurrentVM = mvm.ThatchingVM;
                }

                if (isWoodChecked == true)
                {
                    mvm.CurrentVM = mvm.WoodVM;
                }


                if (isVariousChecked == true)
                {
                    mvm.CurrentVM = mvm.VariousVM;
                }
            }
        }
    }
}

