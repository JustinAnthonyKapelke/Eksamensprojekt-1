using Nordlangelands_Tækkemand.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Nordlangelands_Tækkemand.Commands.StorageCommands
{
    public class UpdateMaterialCommand : ICommand
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
            {
                int materialID = mvm.SelectedMaterial.MaterialID;
                string materialName = mvm.SelectedMaterial.MaterialName;
                string materialDescription = mvm.SelectedMaterial.MaterialDescription;
                int storageID = mvm.SelectedMaterial.StorageID;
                int materialStockCount = mvm.SelectedMaterial.MaterialStockCount;
                bool thatchingIsChecked = mvm.UpdateIsThatchingChecked;
                bool woodIsChecked = mvm.UpdateIsWoodChecked;
                bool variousIsChecked = mvm.UpdateIsVariousChecked;
                string materialType = mvm.SelectedMaterial.MaterialType;

                if (string.IsNullOrWhiteSpace(materialName))
                {
                    MessageBox.Show("Indtast et gyldigt navn");
                    return; // Exit the method if the stock count is not a number
                }

                //Thatching
                if (thatchingIsChecked == true)
                {
                    int materialTypeID = 1;
                    var woodMaterialToRemove = mvm.WoodVM.FirstOrDefault(m => m.MaterialID == materialID);
                    if (woodMaterialToRemove != null)
                    {
                        mvm.WoodVM.Remove(woodMaterialToRemove);
                    }
                    var variousMaterialToRemove = mvm.VariousVM.FirstOrDefault(m => m.MaterialID == materialID);
                    if (variousMaterialToRemove != null)
                    {
                        mvm.VariousVM.Remove(variousMaterialToRemove);
                    }
                    mvm.TVM.UpdateMaterialInDatabase(materialID, materialName, materialDescription, materialTypeID, materialStockCount, storageID);
                    mvm.TVM.ClearMaterialsInRepo();
                    mvm.ThatchingVM.Clear();
                    mvm.TVM.InitializeMaterials();
                    mvm.InitializeThatchingVM();
                }

             
                //Wood
                if (woodIsChecked == true)
                {
                    int materialTypeID = 2;

                    var thatchingMaterialToRemove = mvm.ThatchingVM.FirstOrDefault(m => m.MaterialID == materialID);
                    if (thatchingMaterialToRemove != null)
                    {
                        mvm.ThatchingVM.Remove(thatchingMaterialToRemove);
                    }

                    var variousMaterialToRemove = mvm.VariousVM.FirstOrDefault(m => m.MaterialID == materialID);
                    if (variousMaterialToRemove != null)
                    {
                        mvm.VariousVM.Remove(variousMaterialToRemove);
                    }

                    mvm.WDVM.UpdateMaterialInDatabase(materialID, materialName, materialDescription, materialTypeID, materialStockCount, storageID);
                    mvm.WDVM.ClearMaterialsInRepo();
                    mvm.WoodVM.Clear();
                    mvm.WDVM.InitializeMaterials();
                    mvm.InitializeWoodVM();   
                }






                //Various

                if (variousIsChecked == true)
                {
                    var thatchingMaterialToRemove = mvm.ThatchingVM.FirstOrDefault(m => m.MaterialID == materialID);
                    if (thatchingMaterialToRemove != null)
                    {
                        mvm.ThatchingVM.Remove(thatchingMaterialToRemove);
                    }

                    var woodMaterialToRemove = mvm.WoodVM.FirstOrDefault(m => m.MaterialID == materialID);
                    if (woodMaterialToRemove != null)
                    {
                        mvm.WoodVM.Remove(woodMaterialToRemove);
                    }

                    int materialTypeID = 3;
                    mvm.VVM.UpdateMaterialInDatabase(materialID, materialName, materialDescription, materialTypeID, materialStockCount, storageID);
                    mvm.VVM.ClearMaterialsInRepo();
                    mvm.VariousVM.Clear();
                    mvm.VVM.InitializeMaterials();
                    mvm.InitializeVariousVM();    
                }


                mvm.LogTextCMD.Execute(mvm);
            }

        }
    }
}

