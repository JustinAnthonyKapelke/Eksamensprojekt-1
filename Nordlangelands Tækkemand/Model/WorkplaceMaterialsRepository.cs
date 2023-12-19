using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nordlangelands_Tækkemand.Model
{
    public class WorkplaceMaterialsRepository
    {
        //Database Connection String
        private string _connectionString = "Server=10.56.8.36; Database=DB_F23_TEAM_06; User Id=DB_F23_TEAM_06; Password=TEAMDB_DB_06; TrustServerCertificate=true";

        //Fields
        private List<WorkplaceMaterial> _workplaceMaterials;

        //Constructor
        public WorkplaceMaterialsRepository() 
        {
            _workplaceMaterials = new List<WorkplaceMaterial>();        
        }

        //Methods
        public List<WorkplaceMaterial> GetAll() 
        {
            return _workplaceMaterials;
        }   
        
        public void ClearMaterialsInRepo()
        {
            _workplaceMaterials.Clear();
        }

        public void InitializeWorkplaceMaterialsByWorkplaceID(int workplaceID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                //Use a stored procedure to prevent sql injection.
                string query = $"SELECT MaterialName, MaterialDescription, StorageID, MaterialID, StockCount  FROM NTCombinedWorkPlaceMaterialView Where WorkplaceID = {workplaceID}";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string materialName = (string)reader["MaterialName"];
                        string materialDescription = (string)reader["MaterialDescription"];
                        int storageID = (int)reader["StorageID"];
                        int materialID = (int)reader["MaterialID"];
                        int workplaceMaterialStockCount = (int)reader["StockCount"];

                        WorkplaceMaterial newWorkplaceMaterial = new (materialID,  materialName, materialDescription, workplaceMaterialStockCount, storageID,  workplaceID);
                        _workplaceMaterials.Add(newWorkplaceMaterial);
                    }
                }
            }
        }
    }
}
