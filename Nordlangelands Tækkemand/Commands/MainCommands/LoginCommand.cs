using Nordlangelands_Tækkemand.Model;
using Nordlangelands_Tækkemand.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                if (string.IsNullOrWhiteSpace(mvm.MainWindowInstance.Username_TextBox.Text) || string.IsNullOrWhiteSpace(mvm.MainWindowInstance.Password_TextBox.Text))
                    return false;
            }
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is MainViewModel mvm)
            {
                var usernameInput = mvm.MainWindowInstance.Username_TextBox.Text;
                var passwordInput = mvm.MainWindowInstance.Password_TextBox.Text;

                User user = mvm.UVM.UserRepo.GetAllUsers().FirstOrDefault(u => u.UserName == usernameInput);


                if (user != null)
                {
                    // Sammenlign det indtastede kodeord med det gemte kodeord
                    if (passwordInput == user.UserPassword)
                    {
                        mvm.MainWindowInstance.StorageTab.IsEnabled = true;
                        mvm.MainWindowInstance.WorkplaceTab.IsEnabled = true;
                        mvm.MainWindowInstance.StorageTab.Focus();
                    }
                }

            }
        }


    }
}
