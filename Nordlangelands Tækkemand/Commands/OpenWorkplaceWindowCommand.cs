using Nordlangelands_Tækkemand.ViewModel;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Nordlangelands_Tækkemand.Commands.WorkplaceCommands
{
    internal class OpenWorkplaceWindowCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public bool CanExecute(object? parameter)
        {
            return parameter is MainViewModel mvm && mvm.SelectedWorkplace != null;
        }

        public void Execute(object? parameter)
        {
            if (parameter is MainViewModel mvm)
            {
                WorkplaceWindow workplaceWindow = new(mvm);
                workplaceWindow.Show();

                //Update workplace materials based on the selected workplace
                if (mvm.SelectedWorkplace != null)
                {
                    // Clear existing materials in repo and observablecollection
                    mvm.WKMVM.ClearMaterialsInRepo();
                    mvm.WorkplaceMaterialsVM.Clear();
                    int selectedWorkplaceID = mvm.SelectedWorkplace.WorkplaceID;
                    mvm.WKMVM.InitializeWorkplaceMaterialsByWorkplaceID(selectedWorkplaceID);                 
                    mvm.InitializeWorkplaceMaterialsVM();
                }
            }
        }
    }
}