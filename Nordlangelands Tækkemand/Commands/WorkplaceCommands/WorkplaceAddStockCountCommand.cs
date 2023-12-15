using Nordlangelands_Tækkemand.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Nordlangelands_Tækkemand.Commands.WorkplaceCommands
{
    internal class WorkplaceAddStockCountCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return parameter is MainViewModel mvm && mvm.WorkplaceSelectedMaterial != null;

        }


        public void Execute(object? parameter)
        {
            if (parameter is MainViewModel mvm)
            {
                int stockcount = mvm.WorkplaceSelectedMaterial.WorkplaceMaterialStockCount++; // ikke færdig - opdaterer kun VM og ikke databasen selvfølgelig
            }
        }
    }
}
