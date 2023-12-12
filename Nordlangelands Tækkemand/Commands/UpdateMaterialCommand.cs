using Nordlangelands_Tækkemand.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Nordlangelands_Tækkemand.Commands
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
                bool? thatchingIsChecked = mvm.MainWindowInstance.UpdateMaterialWindow.ThatchingTypeRadioButton.IsChecked;
                bool? woodIsChecked = mvm.MainWindowInstance.UpdateMaterialWindow.WoodTypeRadioButton.IsChecked;
                bool? variousIsChecked = mvm.MainWindowInstance.UpdateMaterialWindow.VariousTypeRadioButton.IsChecked;
                string materialType = mvm.SelectedMaterial.MaterialType;

                if (string.IsNullOrWhiteSpace(materialName))
                {
                    MessageBox.Show("Indtast et gyldigt navn");
                    return; // Exit the method if the stock count is not a number
                }

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

                    mvm.TVM.thatchingRepo.UpdateMaterialInDatabase(materialID, materialName, materialDescription, materialTypeID, materialStockCount, storageID);
                    mvm.TVM.thatchingRepo.ClearMaterialsInRepo();
                    mvm.ThatchingVM.Clear();
                    mvm.TVM.thatchingRepo.InitializeMaterials();
                    mvm.InitializeThatchingVM();

                    mvm.MainWindowInstance.UpdateMaterialWindow.Close();
                }

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

                    mvm.WDVM.woodRepo.UpdateMaterialInDatabase(materialID, materialName, materialDescription, materialTypeID, materialStockCount, storageID);
                    mvm.WDVM.woodRepo.ClearMaterialsInRepo();
                    mvm.WoodVM.Clear();
                    mvm.WDVM.woodRepo.InitializeMaterials();
                    mvm.InitializeWoodVM();
                    mvm.MainWindowInstance.UpdateMaterialWindow.Close();
                }

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
                    mvm.VVM.variousRepo.UpdateMaterialInDatabase(materialID, materialName, materialDescription, materialTypeID, materialStockCount, storageID);
                    mvm.VVM.variousRepo.ClearMaterialsInRepo();
                    mvm.VariousVM.Clear();
                    mvm.VVM.variousRepo.InitializeMaterials();
                    mvm.InitializeVariousVM();
                    mvm.MainWindowInstance.UpdateMaterialWindow.Close();
                }

                mvm.LogTextCMD.Execute(mvm);
            }

        }
    }
}

