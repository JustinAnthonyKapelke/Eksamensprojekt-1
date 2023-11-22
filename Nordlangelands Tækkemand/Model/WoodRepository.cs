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
    //The class implements the IRepository interface
    public class WoodRepository : BaseRepository<WoodMaterial>
    {
        //private string _connectionString = "Server=10.56.8.36; Database=DB_F23_TEAM_06; User Id=DB_F23_TEAM_06; Password=TEAMDB_DB_06; TrustServerCertificate=true";

        //Constructor
        public WoodRepository(CreateMaterialDelegate<WoodMaterial> createDelegate) : base(createDelegate)
        {
            InitializeWoodMaterials();
        }

        //Initialize Wood Materials Method
        public void InitializeWoodMaterials()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT WoodID, WoodName, WoodDescription, WoodStorageIndex, WoodPrice FROM WoodMaterial";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int materialID = (int)reader["WoodID"];
                        string materialName = (string)reader["WoodName"];
                        string materialDescription = (string)reader["WoodDescription"];
                        int materialStorageIndex = (int)reader["WoodStorageIndex"];
                        double MaterialPrice = (double)reader["WoodPrice"];

                        _materials.Add(new WoodMaterial(materialID, materialName, materialDescription, materialStorageIndex, MaterialPrice));
                    }
                }
            }
        }
      
    }
}
