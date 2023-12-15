using Nordlangelands_Tækkemand.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Nordlangelands_Tækkemand.Commands.StorageCommands
{
    class RemoveStockCountCommand : ICommand
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
            if (parameter is MainViewModel mvm)

            //Tager kun imod tal ELLER tektsboks kan kun tage imod tal (reagerer ikke på bogstaver og tegn)
            {
                int newStockCountAmount = int.Parse(mvm.MainWindowInstance.NewStockCountTextBox.Text);
                newStockCountAmount = newStockCountAmount * -1;
                int selectedMaterialID = mvm.SelectedMaterial.MaterialID;

                object currentVM = mvm.CurrentVM;


                if (currentVM == mvm.ThatchingVM && mvm.SelectedMaterial.MaterialStockCount + newStockCountAmount >= 0)
                {
                    // Update material stock count in database
                    mvm.TVM.thatchingRepo.UpdateStockCountInDatabase(selectedMaterialID, newStockCountAmount);

                    // Retrieve the updated material
                    var updatedMaterial = mvm.TVM.thatchingRepo.ReadMaterialByIDFromDatabase(selectedMaterialID);

                    if (updatedMaterial != null)
                    {
                        mvm.SelectedMaterial.MaterialStockCount = updatedMaterial.MaterialStockCount;
                    }
                }

                //woooooody
                if (currentVM == mvm.WoodVM && mvm.SelectedMaterial.MaterialStockCount + newStockCountAmount >= 0)
                {
                    // Update material stock count in database
                    mvm.WDVM.woodRepo.UpdateStockCountInDatabase(selectedMaterialID, newStockCountAmount);

                    // Retrieve the updated material
                    var updatedMaterial = mvm.WDVM.woodRepo.ReadMaterialByIDFromDatabase(selectedMaterialID);

                    if (updatedMaterial != null)
                    {
                        mvm.SelectedMaterial.MaterialStockCount = updatedMaterial.MaterialStockCount;
                    }
                }

                //vari
                if (currentVM == mvm.VariousVM && mvm.SelectedMaterial.MaterialStockCount + newStockCountAmount >= 0)
                {
                    // Update material stock count in database
                    mvm.VVM.variousRepo.UpdateStockCountInDatabase(selectedMaterialID, newStockCountAmount);

                    // Retrieve the updated material
                    var updatedMaterial = mvm.VVM.variousRepo.ReadMaterialByIDFromDatabase(selectedMaterialID);

                    if (updatedMaterial != null)
                    {
                        mvm.SelectedMaterial.MaterialStockCount = updatedMaterial.MaterialStockCount;
                    }
                }

                //vari
                if (currentVM == mvm.AllMaterialsVM && mvm.SelectedMaterial.MaterialStockCount + newStockCountAmount >= 0)
                {
                    // Update material stock count in database
                    mvm.VVM.variousRepo.UpdateStockCountInDatabase(selectedMaterialID, newStockCountAmount);

                    // Retrieve the updated material
                    var updatedMaterial = mvm.VVM.variousRepo.ReadMaterialByIDFromDatabase(selectedMaterialID);

                    if (updatedMaterial != null)
                    {
                        mvm.SelectedMaterial.MaterialStockCount = updatedMaterial.MaterialStockCount;
                    }
                }
            }
        }
    }
}
