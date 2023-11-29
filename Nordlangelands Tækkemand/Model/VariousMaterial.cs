using Nordlangelands_Tækkemand.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordlangelands_Tækkemand.Model
{
    //The class implements the IMaterial interface
    public class VariousMaterial : IMaterial
    {
        //Auto-Implemented Properties
        public int MaterialID { get; set; }
        public string MaterialName { get; set; }
        public string MaterialDescription { get; set; }
        public int MaterialStorageIndex { get; set; }
        public double MaterialPrice { get; set; }
        public int StorageID { get; set; }

        //Constructor
        public VariousMaterial(int materialID, string materialName, string materialDescription, int materialStorageIndex, double materialPrice, int storageID)
        {
            MaterialID = materialID;
            MaterialName = materialName;
            MaterialDescription = materialDescription;
            MaterialStorageIndex = materialStorageIndex;
            MaterialPrice = materialPrice;
            StorageID = storageID;
        }

        public VariousMaterial(string materialName, string materialDescription, int materialStorageIndex, double materialPrice, int storageID)
        {            
            MaterialName = materialName;
            MaterialDescription = materialDescription;
            MaterialStorageIndex = materialStorageIndex;
            MaterialPrice = materialPrice;
            StorageID = storageID;
        }
    }
}
