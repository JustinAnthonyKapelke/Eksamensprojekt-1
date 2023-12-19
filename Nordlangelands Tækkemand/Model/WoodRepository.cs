using Microsoft.Data.SqlClient;
using Nordlangelands_Tækkemand.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Nordlangelands_Tækkemand.Model
{
    //The class inherits the BaseRepository class
    public class WoodRepository : BaseRepository<WoodMaterial>
    {
        //Database Query
        protected override string MaterialType { get; set; } = "Træ";

        //Constructor
        public WoodRepository(CreateDelegate<WoodMaterial> createDelegate) : base(createDelegate)
        {

        }

        //Constructor Overload
        public WoodRepository(CreateDelegate<WoodMaterial> createDelegate, InitializeDelegate<WoodMaterial> initializeCreateDelegate) : base(createDelegate, initializeCreateDelegate)
        {

        }
    }
}
