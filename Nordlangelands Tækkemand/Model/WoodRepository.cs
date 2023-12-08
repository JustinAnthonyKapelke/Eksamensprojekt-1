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
        protected override string RepoInitializeQuery { get; set; } = "SELECT * FROM NTCombinedMaterialView";
        protected override string RepoCreateQuery { get; set; } = "EXEC sp_NTCreateMaterial @MaterialName, @MaterialDescription, @MaterialStockCount, @MaterialTypeID, @StorageID;";
        protected override string RepoReadQuery { get; set; } = "EXEC sp_NTReadLastMaterial";
        protected override string RepoUpdateQuery { get; set; }
        protected override string RepoDeleteQuery { get; set; } = "EXEC sp_NTDeleteMaterial @MaterialID";
        protected override string UpdateStockCountQuery { get; set; } = "EXEC sp_NTUpdateStockCount @MaterialID, @NewMaterialAmount";

        protected override string ReadMaterialByIDQuery { get; set; } = "EXEC sp_NTGetMaterialByID @MaterialID";
        protected override string MaterialType { get; set; } = "Træ";

        //Constructor
        public WoodRepository(CreateDelegate<WoodMaterial> createDelegate) : base(createDelegate)
        {

        }

        //Constructor Overload
        public WoodRepository(CreateDelegate<WoodMaterial> createDelegate, InitializeCreateDelegate<WoodMaterial> initializeCreateDelegate) : base(createDelegate, initializeCreateDelegate)
        {

        }
    }
}
