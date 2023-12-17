using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordlangelands_Tækkemand.Interfaces
{
    public interface IMaterialViewModel
    {
        //Properties
        public int MaterialID { get; set; }
        public string MaterialName { get; set; }
        public string MaterialDescription { get; set; }
        public string MaterialImagePath { get; set; }
        public string MaterialType { get; set; }
        public int MaterialTypeID { get; set; }
        public int MaterialStockCount { get; set; }
        public int StorageID { get; set; }

        //Methods
    

    }
}
