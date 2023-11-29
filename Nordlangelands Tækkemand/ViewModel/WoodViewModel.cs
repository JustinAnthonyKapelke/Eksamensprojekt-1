using Nordlangelands_Tækkemand.Interfaces;
using Nordlangelands_Tækkemand.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Nordlangelands_Tækkemand.ViewModel
{
    public class WoodViewModel
    {
        public WoodRepository woodRepo;

        //Auto-Implemented Properties  
        public int MaterialID { get; set; }
        public string MaterialName { get; set; }
        public string MaterialDescription { get; set; }
        public int MaterialStorageIndex { get; set; }
        public double MaterialPrice { get; set; }
        public int StorageID { get; set; }

        //Constructor
        public WoodViewModel()
        {
            woodRepo = new WoodRepository(CreateDelegate);
        }

        public WoodMaterial CreateDelegate(string materialName, string materialDescription, int materialStorageIndex, double materialPrice, int storageID)
        {
            return new WoodMaterial(materialName, materialDescription, materialStorageIndex, materialPrice, storageID);
        }

        public WoodMaterial InitializeCreateDelegate(int materialID, string materialName, string materialDescription, int materialStorageIndex, double materialPrice, int storageID)
        {
            return new WoodMaterial(materialID, materialName, materialDescription, materialStorageIndex, materialPrice, storageID);
        }

        //Method
        public void CreateAndInsertMaterial(string materialName, string materialDescription, int materialStorageIndex, double materialPrice, int storageID)
        {
            woodRepo.CreateMaterial(materialName, materialDescription, materialStorageIndex, materialPrice, storageID);
            woodRepo.InsertMaterialIntoDatabase(materialName, materialDescription, materialStorageIndex, materialPrice, storageID);
        }
    }
}
