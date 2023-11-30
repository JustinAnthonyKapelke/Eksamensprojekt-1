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
        public ThatchingRepository thatchingRepo;

        //Auto-Implemented Properties
        public int MaterialID { get; set; }
        public string MaterialName { get; set; }
        public string MaterialDescription { get; set; }
        public string MaterialImagePath { get; set; }
        public int MaterialStockCount { get; set; }
        public int MaterialTypeID { get; set; }
        public int StorageID { get; set; }

        //Constructor
        public ThatchingViewModel(ThatchingMaterial material)
        {
            // Initialize properties using the ThatchingMaterial object
            MaterialName = material.MaterialName;
            MaterialDescription = material.MaterialDescription;
            MaterialImagePath = material.MaterialImagePath;
            MaterialStockCount = material.MaterialStockCount;
            MaterialTypeID = material.MaterialTypeID;
            StorageID = material.StorageID;

            thatchingRepo = new ThatchingRepository(CreateDelegate, InitializeCreateDelegate);
        }

        public ThatchingViewModel()
        {         

            thatchingRepo = new ThatchingRepository(CreateDelegate, InitializeCreateDelegate);
        }




        public ThatchingMaterial CreateDelegate(string materialName, string materialDescription, string materialImagePath, int materialStockCount, int materialTypeID, int storageID)
        {
            return new ThatchingMaterial(materialName, materialDescription, materialImagePath, materialStockCount, materialTypeID, storageID);
        }

        public ThatchingMaterial InitializeCreateDelegate(int materialID, string materialName, string materialDescription, string materialImagePath, int materialStockCount, int materialTypeID, int storageID)
        {
            return new ThatchingMaterial(materialID, materialName, materialDescription, materialImagePath, materialStockCount, materialTypeID, storageID);
        }

        //Method
        //public void CreateMaterial(string materialName, string materialDescription, int materialStockCount, double materialPrice, int storageID)
        //{
        //    thatchingRepo.CreateMaterialInRepository(materialName, materialDescription, materialStockCount, storageID);
        //    thatchingRepo.CreateMaterialInDatabase(materialDescription, materialStockCount, storageID);
        //}
    }
}
