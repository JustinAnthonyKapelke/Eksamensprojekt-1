using Nordlangelands_Tækkemand.Model;
using Nordlangelands_Tækkemand.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Nordlangelands_Tækkemand.Commands.MainCommands
{
    public class LoginCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            if (parameter is MainViewModel mvm)
            {
                if (string.IsNullOrWhiteSpace(mvm.UserName) || string.IsNullOrWhiteSpace(mvm.UserPassword))
                    return false;
            }
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is MainViewModel mvm)
            {
                var usernameInput = mvm.UserName;
                var passwordInput = mvm.UserPassword;

                UserViewModel userViewModel = mvm.UserVM.FirstOrDefault(u => u.UserName == usernameInput);
                if (userViewModel != null)
                {
                    // Sammenlign det indtastede kodeord med det gemte kodeord
                    if (passwordInput == userViewModel.UserPassword)
                    {
                        mvm.StorageTabIsEnabled = true;
                        mvm.WorkplaceTabIsEnabled = true;
                        // Set focus to StorageTab
                        mvm.SelectedTabIndex = 1;
                    }
                }
                else
                {
                    // Incorrect input
                    MessageBox.Show("Ugyldigt brugernavn eller adgangskode");
                }
            }
        }


    }
}
