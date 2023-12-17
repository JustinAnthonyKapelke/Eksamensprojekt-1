using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordlangelands_Tækkemand.Model
{
    public class WorkplaceMaterial
    {
        //Auto-Implemented Properties
        public int MaterialID { get; private set; }
        public string MaterialName { get; private set; }
        public string MaterialDescription { get; private set;}
        public int WorkplaceMaterialStockCount { get; private set; }
        public int StorageID { get; private set; }
        public int WorkplaceID { get; private set; }


        //Constructor
        public WorkplaceMaterial() {}

        // Constructor overload
        public WorkplaceMaterial(int materialID, string materialName, string materialDescription, int materialStockCount, int storageID, int workplaceID)
        {
            MaterialID = materialID;
            MaterialName = materialName;
            MaterialDescription = materialDescription;
            WorkplaceMaterialStockCount = materialStockCount;
            StorageID = storageID;
            WorkplaceID = workplaceID;
        }
    }
}
