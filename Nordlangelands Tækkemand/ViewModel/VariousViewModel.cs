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
        public int MaterialStockCount { get; set; }
        public string MaterialType { get; set; }
        public double StorageID { get; set; }

        //Constructor
        public VariousViewModel()
        {
            variousRepo = new VariousRepository(CreateDelegate/*, InitializeCreateDelegate*/);
        }

        public VariousMaterial CreateDelegate(string materialName, string materialDescription, string materialImagePath, int materialStockCount, string materialType, int storageID)
        {
            return new VariousMaterial(materialName, materialDescription,materialImagePath, materialStockCount, materialType, storageID);
        }

        public VariousMaterial InitializeCreateDelegate(int materialID, string materialName, string materialDescription, string materialImagePath, int materialStockCount, string materialType, int storageID)
        {
            return new VariousMaterial(materialID, materialName, materialDescription,materialImagePath, materialStockCount, materialType, storageID);
        }

        //Method
        //public void CreateAndInsertMaterial(string materialName, string materialDescription, int materialStockCount, int storageID)
        //{
        //    variousRepo.CreateMaterial(materialName, materialDescription, materialStockCount, storageID);
        //    variousRepo.InsertMaterialIntoDatabase(materialName, materialDescription, materialStockCount, storageID);
        //}
    }
}
