using Nordlangelands_Tækkemand.Model;
using Nordlangelands_Tækkemand.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;


namespace Nordlangelands_Tækkemand.Commands
{
    public class CreateMaterialCommand : ICommand
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
                //Tilgå CreateMaterialWindow igennem MainViewModelinstansen mvm
                var createMaterialWindow = mvm.MainWindowInstance.CreateMaterialWindow;

                //Gem textinput i lokale variabler
                string name = createMaterialWindow.NameTextBox.Text;
                string description = createMaterialWindow.DescriptionTextBox.Text;
                int stockCount = int.Parse(createMaterialWindow.StockCountTextBox.Text);
                int storageID = int.Parse(createMaterialWindow.StorageIDTextBox.Text);

                bool? isThatchingIsChecked = mvm.MainWindowInstance.CreateMaterialWindow.ThatchingTypeRadioButton.IsChecked;
                bool? isWoodIsChecked = mvm.MainWindowInstance.CreateMaterialWindow.WoodTypeRadioButton.IsChecked;
                bool? isVariousIsChecked = mvm.MainWindowInstance.CreateMaterialWindow.VariousTypeRadioButton.IsChecked;

                // Tildele lokal variablen message en default værdi
                string message = "";

                if (isThatchingIsChecked == true)
                {
                    int materialTypeID = 1;
                    // Create the new material in the database
                    mvm.TVM.thatchingRepo.CreateMaterialInDatabase(name, description, stockCount, materialTypeID, storageID);
                    // Retrieve the newly added material from the database
                    ThatchingMaterial newThatchingMaterial = mvm.TVM.thatchingRepo.ReadLastAddedMaterialFromDatabase();
                    // Create a new ThatchingViewModel with the new material
                    ThatchingViewModel newThatchingVM = new ThatchingViewModel(newThatchingMaterial);
                    // Add the new ThatchingViewModel to the ObservableCollection
                    mvm.ThatchingVM.Add(newThatchingVM);
                    mvm.AllMaterialsVM.Add(newThatchingVM);
                    // Define the displayed type
                    message = "Tække";
                }

                if (isWoodIsChecked == true)
                {
                    int materialTypeID2 = 2;
                    // Create the new material in the database
                    mvm.WDVM.woodRepo.CreateMaterialInDatabase(name, description, stockCount, materialTypeID2, storageID);
                    // Retrieve the newly added material from the database
                    WoodMaterial newWoodMaterial = mvm.WDVM.woodRepo.ReadLastAddedMaterialFromDatabase();
                    // Create a new ThatchingViewModel with the new material
                    WoodViewModel newWoodVM = new WoodViewModel(newWoodMaterial);
                    // Add the new ThatchingViewModel to the ObservableCollection
                    mvm.WoodVM.Add(newWoodVM);
                    mvm.AllMaterialsVM.Add(newWoodVM);
                    // Define the displayed type                        
                    message = "Træ";
                }

                if (isVariousIsChecked == true)
                {

                    int materialTypeID3 = 3;
                    // Create the new material in the database
                    mvm.VVM.variousRepo.CreateMaterialInDatabase(name, description, stockCount, materialTypeID3, storageID);
                    // Retrieve the newly added material from the database
                    VariousMaterial newVariousMaterial = mvm.VVM.variousRepo.ReadLastAddedMaterialFromDatabase();
                    // Create a new ThatchingViewModel with the new material
                    VariousViewModel newVariousVM = new VariousViewModel(newVariousMaterial);
                    // Add the new ThatchingViewModel to the ObservableCollection
                    mvm.VariousVM.Add(newVariousVM);
                    mvm.AllMaterialsVM.Add(newVariousVM);
                    // Define the displayed type
                    message = "Diverse";
                }


                MessageBox.Show($"Dit materiale med typen: '{message}' og navnet: '{name}' er oprettet i systemet.");
            }

        }

    }
}

