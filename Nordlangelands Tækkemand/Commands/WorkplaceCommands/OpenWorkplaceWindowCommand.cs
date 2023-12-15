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
                Thread databaseInteraction = new Thread(() =>
                {
                    // Clear existing materials in repo and observablecollection
                    mvm.WKMVM.WorkplaceMaterialRepo.ClearMaterialsInRepo();

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        mvm.WorkplaceMaterialsVM.Clear();
                        mvm.MainWindowInstance.WorkplaceWindow.Show();
                    }
                    );


                    int selectedWorkplaceID = mvm.SelectedWorkplace.WorkplaceID;
                    mvm.WKMVM.WorkplaceMaterialRepo.InitializeWorkplaceMaterials(selectedWorkplaceID);

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        mvm.InitializeWorkplaceMaterialsVM();
                    }
                    );
                }
                );
                databaseInteraction.Start();
            }
        }
    }
}