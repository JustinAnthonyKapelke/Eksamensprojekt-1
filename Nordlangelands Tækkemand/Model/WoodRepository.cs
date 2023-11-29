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
        //Overwrite RepoQuery Inherited From BaseRepository
        protected override string RepoCreateQuery { get; set; } = "EXEC sp_NTReadWoodMaterial";
        protected override string RepoReadQuery { get; set; } = "EXEC sp_NTReadWoodMaterial";

        //Constructor
        public WoodRepository(CreateDelegate<WoodMaterial> createDelegate) : base(createDelegate)
        {

        }  
    }
}
