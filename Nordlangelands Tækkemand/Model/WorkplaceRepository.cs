using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client.Extensions.Msal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Nordlangelands_Tækkemand.Model
{
    public class WorkplaceRepository
    {
        //Database Connection String
        private string _connectionString = "Server=10.56.8.36; Database=DB_F23_TEAM_06; User Id=DB_F23_TEAM_06; Password=TEAMDB_DB_06; TrustServerCertificate=true";

        //List of workplaces
        private List<Workplace> _workplaces;

        //Constructor
        public WorkplaceRepository()
        {
            InitializeWorkplaces();
            _workplaces = new List<Workplace>();
        }

        //CRUD Methods
        public void CreateWorkplace(string workplaceName, string workplaceAddress, int storageID)
        {
            Workplace newWorkplace = new Workplace(storageID, workplaceName, workplaceAddress);
            _workplaces.Add(newWorkplace);

        }

        public Workplace ReadWorkplace(int workplaceID)
        {
            return _workplaces.FirstOrDefault(w => w.WorkplaceID == workplaceID);
        }

        public void UpdateWorkplace(int workplaceID, string workplaceName, string workplaceAddress)
        {
            Workplace updatedWorkplace = _workplaces.FirstOrDefault(w => w.WorkplaceID == workplaceID);

            if (updatedWorkplace != null)
            {
                updatedWorkplace.WorkplaceName = workplaceName;
                updatedWorkplace.WorkplaceAddress = workplaceAddress;
            }
        }

        public void DeleteWorkplace(int workplaceID)
        {
            Workplace deletedWorkplace = _workplaces.FirstOrDefault(w => w.WorkplaceID == workplaceID);

            if (deletedWorkplace != null)
            {
                _workplaces.Remove(deletedWorkplace);
            }
        }

        //Database Operation Methods
        //Initialize Workplaces From Database
        public void InitializeWorkplaces()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT WorkplaceID, WorkplaceName, WorkplaceAddress FROM NTWorkplace";

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int workplaceID = (int)reader["WoodID"];
                        string workplaceName = (string)reader["WoodName"];
                        string workplaceAddress = (string)reader["WoodDescription"];
                        _workplaces.Add(new Workplace(workplaceID, workplaceName, workplaceAddress));
                    }
                }
            }
        }

        //Insert Workplace Into Database
        public void InsertWorkplaceIntoDatabase(string workplaceName, string workplaceAddress, int storageID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                //Use a stored procedure to prevent an sql injection
                string query = "EXEC sp_CreateWorkplace @WorkplaceName, @WooodDescription, @StorageID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@WorkplaceName", workplaceName);
                    command.Parameters.AddWithValue("@WooodDescription", workplaceAddress);
                    command.Parameters.AddWithValue("@StorageID", storageID);
                    command.ExecuteNonQuery();
                }
            }
        }

        //Update Workplace In Database
        //Delete Workplace From Database
    }
}

