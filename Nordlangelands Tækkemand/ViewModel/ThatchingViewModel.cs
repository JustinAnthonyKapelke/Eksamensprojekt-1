using Nordlangelands_Tækkemand.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Nordlangelands_Tækkemand.ViewModel
{
    public class ThatchingViewModel
    {
        ThatchingRepository thatchingRepo;

        //Auto-Implemented Properties
        public int MaterialID { get; set; }
        public string MaterialName { get; set; }
        public string MaterialDescription { get; set; }
        public int MaterialStorageIndex { get; set; }
        public double MaterialPrice { get; set; }

        //Constructor
        public ThatchingViewModel() 
        {
            thatchingRepo = new ThatchingRepository(CreateDelegate, InitializeCreateDelegate);
        }

        public ThatchingMaterial CreateDelegate(string materialName, string materialDescription, int materialStorageIndex, double materialPrice, int storageID)
        {
            return new ThatchingMaterial(materialName, materialDescription, materialStorageIndex, materialPrice, storageID);
        }

        public ThatchingMaterial InitializeCreateDelegate(int materialID, string materialName, string materialDescription, int materialStorageIndex, double materialPrice, int storageID)
        {
            return new ThatchingMaterial(materialID, materialName, materialDescription, materialStorageIndex, materialPrice, storageID);
        }

        //Method
        public void CreateAndInsertMaterial(string materialName, string materialDescription, int materialStorageIndex, double materialPrice, int storageID)
        {
            thatchingRepo.CreateMaterial(materialName, materialDescription, materialStorageIndex, materialPrice, storageID);
            thatchingRepo.InsertMaterialIntoDatabase(materialName, materialDescription, materialStorageIndex, materialPrice, storageID);
        }
    }
}
