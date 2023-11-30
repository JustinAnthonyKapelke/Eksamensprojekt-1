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
        public int MaterialStockCount { get; set; }
        public string MaterialType { get; set; }
        public int StorageID { get; set; }

        //Constructor
        public WoodViewModel()
        {
            woodRepo = new WoodRepository(CreateDelegate);
        }

        public WoodMaterial CreateDelegate(string materialName, string materialDescription, string materialImagePath, int materialStockCount, int materialTypeID, int storageID)
        {
            return new WoodMaterial(materialName, materialDescription, materialImagePath, materialStockCount, materialTypeID, storageID);
        }

        public WoodMaterial InitializeCreateDelegate(int materialID, string materialName, string materialDescription, string materialImagePath, int materialStockCount, int materialTypeID, int storageID)
        {
            return new WoodMaterial(materialID, materialName, materialDescription, materialImagePath, materialStockCount, materialTypeID, storageID);
        }
    }
}

//Method
//    public void CreateAndInsertMaterial(string materialName, string materialDescription, int materialStockCount, int storageID)
//    {
//        woodRepo.CreateMaterial(materialName, materialDescription, materialStockCount, storageID);
//        woodRepo.InsertMaterialIntoDatabase(materialName, materialDescription, materialStockCount, storageID);
//    }
//}
//}
