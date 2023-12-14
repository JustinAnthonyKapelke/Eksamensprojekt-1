using Nordlangelands_Tækkemand.ViewModel;
using System;
using System.Windows.Input;

namespace Nordlangelands_Tækkemand.Commands
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
                mvm.MainWindowInstance.WorkplaceWindow.Show();                
                int selectedWorkplaceID = mvm.SelectedWorkplace.WorkplaceID;
                mvm.WKMVM.WorkplaceMaterialRepo.InitializeWorkplaceMaterials(selectedWorkplaceID);
                mvm.InitializeWorkplaceMaterialsVM();
            }
        }
    }
}