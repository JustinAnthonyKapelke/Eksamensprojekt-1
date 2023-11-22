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
    //The class implements the IRepository interface
    public class ThatchingRepository : BaseRepository<ThatchingMaterial>
    {
        //Constructor
        public ThatchingRepository(CreateMaterialDelegate<ThatchingMaterial> createDelegate) : base(createDelegate)
        {
           InitializeThatchingMaterials();
        }

        //Initialize Thatching Materials Method
        public void InitializeThatchingMaterials()
        {
            string connectionString = "din forbindelsesstreng her"; // Erstat med din database forbindelsesstreng

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseServerInstance"].ConnectionString))
            {
                connection.Open();

                string query = "SELECT ThatchingID, ThatchingName, ThatchingDescription, ThatchingStorageIndex, ThatchingReader FROM ThatchingMaterial";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int materialID = (int)reader["ThatchingID"];
                        string materialName = (string)reader["ThatchingName"];
                        string materialDescription = (string)reader["ThatchingDescription"];
                        int materialStorageIndex = (int)reader["ThatchingStorageIndex"];
                        double MaterialPrice = (double)reader["ThatchingReader"];

                        _materials.Add(new ThatchingMaterial(materialID, materialName, materialDescription, materialStorageIndex, MaterialPrice));
                    }
                }
            }
        }
    }
}
