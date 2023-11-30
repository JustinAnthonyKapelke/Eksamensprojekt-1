using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordlangelands_Tækkemand.Model
{
    public class Workplace
    {
        //Auto-Implemented Properties
        public int WorkplaceID { get; private set; }
        public string WorkplaceName { get; set; }
        public string WorkplaceAddress { get; set; }
        public string WorkplaceImagePath { get; set; }
        public int StorageID { get; set; }

        //Constructor
        public Workplace(string workplaceName, string workplaceAddress, string workplaceImagePath, int storageID)
        {
            WorkplaceName = workplaceName;
            WorkplaceAddress = workplaceAddress;
            WorkplaceImagePath = workplaceImagePath;
            StorageID = storageID;
        }

        //Constructor Overload
        public Workplace(int workplaceID, string workplaceName, string workplaceAddress, string workplaceImagePath, int storageID)
        {
            WorkplaceID = workplaceID;
            WorkplaceName = workplaceName;
            WorkplaceAddress = workplaceAddress;
            WorkplaceImagePath = workplaceImagePath;
            StorageID = storageID;
        }
    }
}
