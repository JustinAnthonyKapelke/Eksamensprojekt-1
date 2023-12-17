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


namespace Nordlangelands_Tækkemand.Commands.StorageCommands
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
                //var createMaterialWindow = mvm.MainWindowInstance.CreateMaterialWindow;

                //Gem textinput i lokale variabler                
                string name = mvm.CreateMaterialName;
                string description = mvm.CreateMaterialDescription;
                string stockCountText = mvm.CreateMaterialStockCount;
                int storageID = mvm.CreateMaterialStorageID;

                bool isThatchingIsChecked = mvm.IsThatchingChecked;
                bool isWoodIsChecked = mvm.IsWoodChecked;
                bool isVariousIsChecked = mvm.IsVariousChecked;

                //Be sure that stockount only can be a number 
                if (!int.TryParse(stockCountText, out int stockCount) || stockCount < 0)
                {
                    MessageBox.Show("Antal skal være et gyldigt tal");
                    return; // Exit the method if the stock count is not a number
                }


                if (name != "" && stockCount >= 0)
                {
                    if (isThatchingIsChecked == true)
                    {
                        int materialTypeID = 1;
                        // Create the new material in the database
                        mvm.TVM.CreateMaterialInDatabase(name, description, stockCount, materialTypeID, storageID);
                        // Retrieve the newly added material from the database
                        ThatchingMaterial newThatchingMaterial = mvm.TVM.ReadLastAddedMaterialFromDatabase();
                        // Create a new ThatchingViewModel with the new material
                        ThatchingViewModel newThatchingVM = new ThatchingViewModel(newThatchingMaterial);
                        // Add the new ThatchingViewModel to the ObservableCollection
                        mvm.ThatchingVM.Add(newThatchingVM);
                        mvm.AllMaterialsVM.Add(newThatchingVM);
                    }
                    else if (isWoodIsChecked == true)
                    {
                        int materialTypeID2 = 2;
                        // Create the new material in the database
                        mvm.WDVM.CreateMaterialInDatabase(name, description, stockCount, materialTypeID2, storageID);
                        // Retrieve the newly added material from the database
                        WoodMaterial newWoodMaterial = mvm.WDVM.ReadLastAddedMaterialFromDatabase();
                        // Create a new ThatchingViewModel with the new material
                        WoodViewModel newWoodVM = new WoodViewModel(newWoodMaterial);
                        // Add the new ThatchingViewModel to the ObservableCollection
                        mvm.WoodVM.Add(newWoodVM);
                        mvm.AllMaterialsVM.Add(newWoodVM);
                    }
                    else if (isVariousIsChecked == true)
                    {
                        int materialTypeID3 = 3;
                        // Create the new material in the database
                        mvm.VVM.CreateMaterialInDatabase(name, description, stockCount, materialTypeID3, storageID);
                        // Retrieve the newly added material from the database
                        VariousMaterial newVariousMaterial = mvm.VVM.ReadLastAddedMaterialFromDatabase();
                        // Create a new ThatchingViewModel with the new material
                        VariousViewModel newVariousVM = new VariousViewModel(newVariousMaterial);
                        // Add the new ThatchingViewModel to the ObservableCollection
                        mvm.VariousVM.Add(newVariousVM);
                        mvm.AllMaterialsVM.Add(newVariousVM);
                    }
                    else
                    {
                        MessageBox.Show("Vælg en materialetype.");
                    }

                }
                else
                {
                    MessageBox.Show("Indtast et gyldigt materialenavn!");
                }

                mvm.LogTextCMD.Execute(mvm);
            }

        }

    }
}

