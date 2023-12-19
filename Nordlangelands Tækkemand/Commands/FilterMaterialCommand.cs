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
                bool isAllChecked = mvm.StorageIsAllChecked;
                bool isThatchingChecked = mvm.StorageIsThatchingChecked;
                bool isWoodChecked = mvm.StorageIsWoodChecked;
                bool isVariousChecked = mvm.StorageIsVariousChecked;

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

