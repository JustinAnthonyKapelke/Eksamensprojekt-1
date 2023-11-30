using Nordlangelands_Tækkemand.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordlangelands_Tækkemand.Model
{
    //The class implements the IMaterial interface
    public class ThatchingMaterial : IMaterial
    {
        //Auto-Implemented Properties
        public int MaterialID { get; set; }
        public string MaterialName { get; set; }
        public string MaterialDescription { get; set; }
        public string MaterialImagePath { get; set; }
        public int MaterialStockCount { get; set; }
        public int MaterialTypeID { get; set; }    
        public int StorageID { get; set; }

        //Constructor
        public ThatchingMaterial()
        {
        }

        public ThatchingMaterial(int materialID, string materialName, string materialDescription, string materialImagePath, int materialStockCount, int materialTypeID, int storageID)
        { 
            MaterialID = materialID;          
            MaterialName = materialName;
            MaterialDescription = materialDescription;
            MaterialImagePath = materialImagePath;
            MaterialStockCount = materialStockCount;
            MaterialTypeID = materialTypeID;
            StorageID = storageID;
        }

        //Constructor Overload
        public ThatchingMaterial(string materialName, string materialDescription, string materialImagePath, int materialStockCount, int materialTypeID, int storageID)
        {
            MaterialName = materialName;
            MaterialDescription = materialDescription;
            MaterialImagePath = materialImagePath;
            MaterialStockCount = materialStockCount;
            MaterialTypeID = materialTypeID;
            StorageID = storageID;
        }

      
    }
}
