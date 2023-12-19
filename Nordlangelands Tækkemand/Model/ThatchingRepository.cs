using Nordlangelands_Tækkemand.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;


namespace Nordlangelands_Tækkemand.Model
{
    //The class inherits the BaseRepository class
    public class ThatchingRepository : BaseRepository<ThatchingMaterial>
    {
        //Database Query      

        protected override string MaterialType { get; set; } = "Tække";

        //Constructor
        public ThatchingRepository(CreateDelegate<ThatchingMaterial> createDelegate) : base(createDelegate)
        {
        }

        //Constructor Overload
        public ThatchingRepository(CreateDelegate<ThatchingMaterial> createDelegate, InitializeDelegate<ThatchingMaterial> initializeCreateDelegate) : base(createDelegate, initializeCreateDelegate)
        {
        }
    }
}
