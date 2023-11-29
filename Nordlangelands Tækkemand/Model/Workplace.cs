﻿using System;
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
        public int StorageID { get; set; }

        //Constructor
        public Workplace(int storageID, string workplaceName, string workplaceAddress)
        {
            StorageID = storageID;
            WorkplaceName = workplaceName;
            WorkplaceAddress = workplaceAddress;
        }

        //Constructor Overload
        public Workplace(string workplaceName, string workplaceAddress)
        {           
            WorkplaceName = workplaceName;
            WorkplaceAddress = workplaceAddress;
        }            
    }
}
