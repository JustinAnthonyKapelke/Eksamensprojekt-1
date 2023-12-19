using Nordlangelands_Tækkemand.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Nordlangelands_Tækkemand.Interfaces;

namespace Nordlangelands_Tækkemand.Commands
{
    class AddStockCountCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return parameter is MainViewModel mvm && mvm.SelectedMaterial != null;
        }

        public void Execute(object? parameter)
        {
            //Tager kun imod tal ELLER tektsboks kan kun tage imod tal (reagerer ikke på bogstaver og tegn)
            if (parameter is MainViewModel mvm)
            {

                try
                {
                    int newStockCountAmount = mvm.NewStockCount;
                    int selectedMaterialID = mvm.SelectedMaterial.MaterialID;

                    object currentVM = mvm.CurrentVM;


                    if (currentVM == mvm.ThatchingVM)
                    {
                        // Update material stock count in database
                        mvm.TVM.UpdateStockCountInDatabase(selectedMaterialID, newStockCountAmount);

                        // Retrieve the updated material
                        var updatedMaterial = mvm.TVM.ReadMaterialByIDFromDatabase(selectedMaterialID);

                        if (updatedMaterial != null)
                        {
                            mvm.SelectedMaterial.MaterialStockCount = updatedMaterial.MaterialStockCount;
                        }
                    }

                    //woooooody
                    if (currentVM == mvm.WoodVM)
                    {
                        // Update material stock count in database
                        mvm.WDVM.UpdateStockCountInDatabase(selectedMaterialID, newStockCountAmount);

                        // Retrieve the updated material
                        var updatedMaterial = mvm.WDVM.ReadMaterialByIDFromDatabase(selectedMaterialID);

                        if (updatedMaterial != null)
                        {
                            mvm.SelectedMaterial.MaterialStockCount = updatedMaterial.MaterialStockCount;
                        }
                    }

                    //vari
                    if (currentVM == mvm.VariousVM)
                    {
                        // Update material stock count in database
                        mvm.VVM.UpdateStockCountInDatabase(selectedMaterialID, newStockCountAmount);

                        // Retrieve the updated material
                        var updatedMaterial = mvm.VVM.ReadMaterialByIDFromDatabase(selectedMaterialID);

                        if (updatedMaterial != null)
                        {
                            mvm.SelectedMaterial.MaterialStockCount = updatedMaterial.MaterialStockCount;
                        }
                    }

                    //AllMaterials
                    if (currentVM == mvm.AllMaterialsVM)
                    {
                        // Update material stock count in database
                        mvm.VVM.UpdateStockCountInDatabase(selectedMaterialID, newStockCountAmount);

                        // Retrieve the updated material
                        var updatedMaterial = mvm.VVM.ReadMaterialByIDFromDatabase(selectedMaterialID);

                        if (updatedMaterial != null)
                        {
                            mvm.SelectedMaterial.MaterialStockCount = updatedMaterial.MaterialStockCount;
                        }
                    }
                }

                catch (Exception)
                {
                    MessageBox.Show("Indtast et gyldigt tal");
                }
            }
        }
    }
}



