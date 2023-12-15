using Nordlangelands_Tækkemand.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Nordlangelands_Tækkemand.Commands.StorageCommands
{
    public class DeleteMaterialCommand : ICommand
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

                string name = mvm.SelectedMaterial.MaterialName;
                int materialID = mvm.SelectedMaterial.MaterialID;
                string type = mvm.SelectedMaterial.MaterialType;
                object currentVM = mvm.CurrentVM;

                // Tildele lokal variablen message en default værdi
                string message = "";


                var result = MessageBox.Show($"Dit materiale med navnet: '{name}' vil blive slettet permanent fra systemet. Er du sikker?", "Bekræft sletning", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    if (currentVM == mvm.ThatchingVM)
                        mvm.TVM.thatchingRepo.DeleteMaterialFromDatabase(materialID);

                    var thatchingMaterialToRemove = mvm.ThatchingVM.FirstOrDefault(m => m.MaterialID == materialID);
                    if (thatchingMaterialToRemove != null)
                    {
                        mvm.ThatchingVM.Remove(thatchingMaterialToRemove);
                    }

                    if (currentVM == mvm.WoodVM)
                        mvm.WDVM.woodRepo.DeleteMaterialFromDatabase(materialID);

                    var woodMaterialToRemove = mvm.WoodVM.FirstOrDefault(m => m.MaterialID == materialID);
                    if (woodMaterialToRemove != null)
                    {
                        mvm.WoodVM.Remove(woodMaterialToRemove);
                    }

                    if (currentVM == mvm.VariousVM)
                        mvm.VVM.variousRepo.DeleteMaterialFromDatabase(materialID);

                    var variousMaterialToRemove = mvm.VariousVM.FirstOrDefault(m => m.MaterialID == materialID);
                    if (variousMaterialToRemove != null)
                    {
                        mvm.VariousVM.Remove(variousMaterialToRemove);
                    }

                    if (currentVM == mvm.AllMaterialsVM)
                        mvm.VVM.variousRepo.DeleteMaterialFromDatabase(materialID);

                    var materialToRemove = mvm.AllMaterialsVM.FirstOrDefault(m => m.MaterialID == materialID);
                    if (materialToRemove != null)
                    {
                        mvm.AllMaterialsVM.Remove(materialToRemove);
                    }
                    mvm.LogTextCMD.Execute(mvm);
                }


                if (result == MessageBoxResult.No)
                {
                }

            }
        }
    }
}

//public void Execute(object? parameter)
//{

//    if (parameter is MainViewModel mvm)
//    {

//        string name = mvm.SelectedMaterial.MaterialName;
//        int materialID = mvm.SelectedMaterial.MaterialID;
//        string type = mvm.SelectedMaterial.MaterialType;


//        // Tildele lokal variablen message en default værdi
//        string message = "";


//        var result = MessageBox.Show($"Dit materiale med navnet: '{name}' vil blive slettet permanent fra systemet. Er du sikker?", "Bekræft sletning", MessageBoxButton.YesNo, MessageBoxImage.Warning);

//        if (result == MessageBoxResult.Yes)
//        {
//            if (type == "tække")
//                mvm.TVM.thatchingRepo.DeleteMaterialFromDatabase(materialID);

//            var thatchingMaterialToRemove = mvm.ThatchingVM.FirstOrDefault(m => m.MaterialID == materialID);
//            if (thatchingMaterialToRemove != null)
//            {
//                mvm.ThatchingVM.Remove(thatchingMaterialToRemove);
//            }

//            if (type == "wood")
//                mvm.WDVM.woodRepo.DeleteMaterialFromDatabase(materialID);

//            var woodMaterialToRemove = mvm.WoodVM.FirstOrDefault(m => m.MaterialID == materialID);
//            if (woodMaterialToRemove != null)
//            {
//                mvm.WoodVM.Remove(woodMaterialToRemove);
//            }

//            if (type == "diverse")
//                mvm.VVM.variousRepo.DeleteMaterialFromDatabase(materialID);

//            var variousMaterialToRemove = mvm.VariousVM.FirstOrDefault(m => m.MaterialID == materialID);
//            if (variousMaterialToRemove != null)
//            {
//                mvm.VariousVM.Remove(variousMaterialToRemove);
//            }
//        }

//        if (result == MessageBoxResult.No)
//        {
//        }

//    }
//}
