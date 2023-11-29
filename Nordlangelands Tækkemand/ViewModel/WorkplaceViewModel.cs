using Nordlangelands_Tækkemand.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordlangelands_Tækkemand.ViewModel
{
    public class WorkplaceViewModel
    {     
        public WorkplaceRepository workplaceRepo;       

        //Auto-Implemented Properties
        public int WorkplaceID { get; private set; }
        public string WorkplaceName { get; set; }
        public string WorkplaceAddress { get; set; }

        //Constructor
        public WorkplaceViewModel()
        {
            workplaceRepo = new WorkplaceRepository();
        }

        //Method
        public void CreateAndInsertMaterial(int storageID, string workplaceName, string workplaceAddress)
        {
            workplaceRepo.CreateWorkplace(workplaceName, workplaceAddress, storageID);
            workplaceRepo.InsertWorkplaceIntoDatabase(workplaceName, workplaceAddress, storageID);
        }
    }
}
