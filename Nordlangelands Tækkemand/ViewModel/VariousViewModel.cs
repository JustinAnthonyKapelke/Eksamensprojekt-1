using Nordlangelands_Tækkemand.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordlangelands_Tækkemand.ViewModel
{
    public class VariousViewModel
    {
        public VariousRepository variousRepo;

        //Auto-Implemented Properties
        public int MaterialID { get; set; }
        public string MaterialName { get; set; }
        public string MaterialDescription { get; set; }
        public int MaterialStorageIndex { get; set; }
        public double MaterialPrice { get; set; }
        public double StorageID { get; set; }

        //Constructor
        public VariousViewModel()
        {
            variousRepo = new VariousRepository(CreateDelegate/*, InitializeCreateDelegate*/);
        }

        public VariousMaterial CreateDelegate(string materialName, string materialDescription, int materialStorageIndex, double materialPrice, int storageID)
        {
            return new VariousMaterial(materialName, materialDescription, materialStorageIndex, materialPrice, storageID);
        }

        public VariousMaterial InitializeCreateDelegate(int materialID, string materialName, string materialDescription, int materialStorageIndex, double materialPrice, int storageID)
        {
            return new VariousMaterial(materialID, materialName, materialDescription, materialStorageIndex, materialPrice, storageID);
        }

        //Method
        public void CreateAndInsertMaterial(string materialName, string materialDescription, int materialStorageIndex, double materialPrice, int storageID)
        {
            variousRepo.CreateMaterial(materialName, materialDescription, materialStorageIndex, materialPrice, storageID);
            variousRepo.InsertMaterialIntoDatabase(materialName, materialDescription, materialStorageIndex, materialPrice, storageID);
        }
    }
}
