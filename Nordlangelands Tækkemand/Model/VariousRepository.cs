using Microsoft.Data.SqlClient;
using Nordlangelands_Tækkemand.Interfaces;
using Nordlangelands_Tækkemand.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordlangelands_Tækkemand.Model
{
    //The class inherits the BaseRepository class
    public class VariousRepository : BaseRepository<VariousMaterial>
    {
        //Database Query  
        protected override string MaterialType { get; set; } = "Diverse";

        //Constructor
        public VariousRepository(CreateDelegate<VariousMaterial> createDelegate) : base(createDelegate)
        {

        }

        //Constructor Overload
        public VariousRepository(CreateDelegate<VariousMaterial> createDelegate, InitializeDelegate<VariousMaterial> initializeCreateDelegate) : base(createDelegate, initializeCreateDelegate)
        {

        }
    }
}

