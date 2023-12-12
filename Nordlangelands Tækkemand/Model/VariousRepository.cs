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
        //Overwrite RepoQuery Inherited From BaseRepository
        protected override string RepoInitializeQuery { get; set; } = "SELECT * FROM NTCombinedMaterialView";
        protected override string RepoCreateQuery { get; set; } = "EXEC sp_NTCreateMaterial @MaterialName, @MaterialDescription, @MaterialStockCount, @MaterialTypeID, @StorageID;";
        protected override string RepoReadQuery { get; set; } = "EXEC sp_NTReadLastMaterial";
        protected override string DatabaseUpdateQuery { get; set; } = "EXEC sp_NTUpdateMaterial @MaterialID, @MaterialName, @MaterialDescription, @MaterialStockCount, @MaterialTypeID, @StorageID";
        protected override string RepoDeleteQuery { get; set; } = "EXEC sp_NTDeleteMaterial @MaterialID";
        protected override string UpdateStockCountQuery { get; set; } = "EXEC sp_NTUpdateStockCount @MaterialID, @NewMaterialAmount";
        protected override string ReadMaterialByIDQuery { get; set; } = "EXEC sp_NTGetMaterialByID @MaterialID";
        protected override string MaterialType { get; set; } = "Diverse";

        //Constructor
        public VariousRepository(CreateDelegate<VariousMaterial> createDelegate) : base(createDelegate)
        {

        }

        //Constructor Overload
        public VariousRepository(CreateDelegate<VariousMaterial> createDelegate, InitializeCreateDelegate<VariousMaterial> initializeCreateDelegate) : base(createDelegate, initializeCreateDelegate)
        {

        }
    }
}

