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

        //Huske rigtig navnestonna
        protected override string RepoInitializeQuery { get; set; } = "SELECT * FROM NTCombinedMaterialView";
        protected override string RepoCreateQuery { get; set; } = "EXEC sp_NTCreateMaterial @MaterialName, @MaterialDescription, @MaterialStockCount, @MaterialTypeID, @StorageID;";
        protected override string RepoReadQuery { get; set; } = "EXEC sp_NTReadLastMaterial";
        protected override string DatabaseUpdateQuery { get; set; } = "EXEC sp_NTUpdateMaterial @MaterialID, @MaterialName, @MaterialDescription, @MaterialStockCount, @MaterialTypeID, @StorageID";
        protected override string RepoDeleteQuery { get; set; } = "EXEC sp_NTDeleteMaterial @MaterialID";
        protected override string UpdateStockCountQuery { get; set; } = "EXEC sp_NTUpdateStockCount @MaterialID, @NewMaterialAmount";

        protected override string ReadMaterialByIDQuery { get; set; } = "EXEC sp_NTGetMaterialByID @MaterialID";

        protected override string MaterialType { get; set; } = "Tække";



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
