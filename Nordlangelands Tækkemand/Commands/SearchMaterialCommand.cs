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
using Nordlangelands_Tækkemand.Interfaces;

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
                object currentVM = mvm.CurrentVM;
                string searchText = mvm.SearchText?.Trim().ToUpper();

                if (currentVM == mvm.ThatchingVM)
                {
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

                //Wood
                if (currentVM == mvm.WoodVM)
                {
                    List<WoodViewModel> filteredWoodMaterials;
                    // Tjekker, om søgekriteriet er tomt og filtrerer baseret på dette
                    if (!string.IsNullOrEmpty(searchText))
                    {

                        //Linq
                        filteredWoodMaterials = mvm.WoodVM
                            .Where(m => m.MaterialName != null && m.MaterialName.ToUpper().Contains(searchText))
                            .ToList();
                    }
                    else
                    {
                        // Hvis søgefeltet er tomt, genindlæs alle materialer
                        mvm.WoodVM.Clear();
                        mvm.InitializeWoodVM();
                        return; // Afslut metoden tidligt for at undgå yderligere operationer
                    }

                    // Opdater ThatchingVM baseret på filtreringen
                    mvm.WoodVM.Clear();

                    //Linq
                    filteredWoodMaterials.ForEach(m => mvm.WoodVM.Add(m));
                }

                //Various
                if (currentVM == mvm.VariousVM)
                {
                    List<VariousViewModel> filteredVariousMaterials;
                    // Tjekker, om søgekriteriet er tomt og filtrerer baseret på dette
                    if (!string.IsNullOrEmpty(searchText))
                    {

                        //Linq
                        filteredVariousMaterials = mvm.VariousVM
                            .Where(m => m.MaterialName != null && m.MaterialName.ToUpper().Contains(searchText))
                            .ToList();
                    }
                    else
                    {
                        // Hvis søgefeltet er tomt, genindlæs alle materialer
                        mvm.VariousVM.Clear();
                        mvm.InitializeVariousVM();
                        return; // Afslut metoden tidligt for at undgå yderligere operationer
                    }

                    // Opdater ThatchingVM baseret på filtreringen
                    mvm.VariousVM.Clear();

                    //Linq
                    filteredVariousMaterials.ForEach(m => mvm.VariousVM.Add(m));
                }

                //AllMaterials
                if (currentVM == mvm.AllMaterialsVM)
                {
                    List<IMaterialViewModel> filteredAllMaterials;
                    // Tjekker, om søgekriteriet er tomt og filtrerer baseret på dette
                    if (!string.IsNullOrEmpty(searchText))
                    {

                        //Linq
                        filteredAllMaterials = mvm.AllMaterialsVM
                            .Where(m => m.MaterialName != null && m.MaterialName.ToUpper().Contains(searchText))
                            .ToList();
                    }
                    else
                    {
                        // Hvis søgefeltet er tomt, genindlæs alle materialer
                        mvm.AllMaterialsVM.Clear();
                        mvm.InitializeAllMaterialsVM();
                        return; // Afslut metoden tidligt for at undgå yderligere operationer
                    }

                    // Opdater ThatchingVM baseret på filtreringen
                    mvm.AllMaterialsVM.Clear();

                    //Linq
                    filteredAllMaterials.ForEach(m => mvm.AllMaterialsVM.Add(m));
                }
            }
        }
    }
}
