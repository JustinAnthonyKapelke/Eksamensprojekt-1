using Nordlangelands_Tækkemand.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Nordlangelands_Tækkemand.Model;
using System.Collections.ObjectModel;

namespace Nordlangelands_Tækkemand.Commands
{

    public class SearchMaterialCommand : ICommand
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
                string searchText = mvm.SearchText?.Trim().ToUpper();

                List<ThatchingViewModel> filteredThatchingMaterials;

                // Tjekker, om søgekriteriet er tomt og filtrerer baseret på dette
                if (!string.IsNullOrEmpty(searchText))
                {
                    //Linq
                    filteredThatchingMaterials = mvm.ThatchingVM
                        .Where(m => m.MaterialName != null && m.MaterialName.ToUpper().Contains(searchText))
                        .ToList();
                }
                else
                {
                    // Hvis søgefeltet er tomt, genindlæs alle materialer
                    mvm.ThatchingVM.Clear();
                    mvm.InitializeThatchingVM();
                    return; // Afslut metoden tidligt for at undgå yderligere operationer
                }

                // Opdater ThatchingVM baseret på filtreringen
                mvm.ThatchingVM.Clear();

                //Linq
                filteredThatchingMaterials.ForEach(m => mvm.ThatchingVM.Add(m));                    
            }
        }
    }
}
