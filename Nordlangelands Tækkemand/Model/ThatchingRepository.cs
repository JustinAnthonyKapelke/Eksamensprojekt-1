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
        //Overwrite RepoQuery Inherited From BaseRepository
        protected override string RepoInitializeQuery { get; set; } = "SELECT * FROM NTMaterial";
        protected override string RepoCreateQuery { get; set; } = "EXEC sp_NTCreateMaterialTest @MaterialName, @MaterialDescription, @MaterialImagePath, @MaterialTypeID, @StorageID";
        protected override string RepoReadQuery { get; set; } = "EXEC sp_NTReadLastMaterial";
        protected override string RepoUpdateQuery { get; set; }
        protected override string RepoDeleteQuery { get; set; } = "EXEC sp_NTDeleteMaterial @MaterialID";

        //Constructor
        public ThatchingRepository(CreateDelegate<ThatchingMaterial> createDelegate) : base(createDelegate)
        {
            
        }

        //Constructor Overload
        public ThatchingRepository(CreateDelegate<ThatchingMaterial> createDelegate, InitializeCreateDelegate<ThatchingMaterial> initializeCreateDelegate) : base(createDelegate, initializeCreateDelegate)
        {
           
        }
    }
}
