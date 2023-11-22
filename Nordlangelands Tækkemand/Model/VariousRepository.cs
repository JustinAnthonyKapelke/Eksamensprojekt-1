using Microsoft.Data.SqlClient;
using Nordlangelands_Tækkemand.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordlangelands_Tækkemand.Model
{
    //The class implements the IRepository interface
    public class VariousRepository : BaseRepository<VariousMaterial>
    {
        //Constructor
        public VariousRepository(CreateMaterialDelegate<VariousMaterial> createDelegate) : base(createDelegate)
        {
            InitializeWoodMaterials();
        }

        //Initialize Various Materials Method
        public void InitializeWoodMaterials()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT VariousID, VariousName, VariousDescription, VariousStorageIndex, VariousPrice FROM VariousMaterial";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int variousID = (int)reader["VariousIDVariousID"];
                        string variousName = (string)reader["VariousName"];
                        string variousDescription = (string)reader["VariousDescription"];
                        int variousStorageIndex = (int)reader["VariousStorageIndex"];
                        double variousPrice = (double)reader["VariousPrice"];

                        _materials.Add(new VariousMaterial(variousID, variousName, variousDescription, variousStorageIndex, variousPrice));
                    }
                }
            }
        }
    }
}
