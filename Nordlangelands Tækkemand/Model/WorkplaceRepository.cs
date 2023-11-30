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

        //Database Queries
        protected virtual string RepoInitializeQuery { get; set; } = "SELECT WorkplaceID, WorkplaceName, WorkplaceAddress, WorkplaceImagePath FROM NTWorkplace";
        protected virtual string RepoCreateQuery { get; set; } = "EXEC sp_CreateWorkplace @WorkplaceName, @WooodAddress, @WorkplaceImagePath, @StorageID";
        protected virtual string RepoReadQuery { get; set; } = "";
        protected virtual string RepoUpdateQuery { get; set; } = "";
        protected virtual string RepoDeleteQuery { get; set; } = "";

        //Constructor
        public WorkplaceRepository()
        {
            InitializeWorkplaces();
            _workplaces = new List<Workplace>();
        }

        //CRUD Methods
        public void CreateWorkplaceInRepository(string workplaceName, string workplaceAddress, string workplaceImagePath, int storageID)
        {
            Workplace newWorkplace = new Workplace(workplaceName, workplaceAddress, workplaceImagePath, storageID);
            _workplaces.Add(newWorkplace);
        }

        public Workplace ReadWorkplaceFromRepository(int workplaceID)
        {
            return _workplaces.FirstOrDefault(w => w.WorkplaceID == workplaceID);
        }

        public void UpdateWorkplaceInRepository(int workplaceID, string workplaceName, string workplaceAddress, string workplaceImagePath)
        {
            Workplace updatedWorkplace = _workplaces.FirstOrDefault(w => w.WorkplaceID == workplaceID);

            if (updatedWorkplace != null)
            {
                updatedWorkplace.WorkplaceName = workplaceName;
                updatedWorkplace.WorkplaceAddress = workplaceAddress;
                updatedWorkplace.WorkplaceImagePath = workplaceImagePath;
            }
        }

        public void DeleteWorkplaceFromRepository(int workplaceID)
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

                string query = RepoInitializeQuery;

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int workplaceID = (int)reader["WorkplaceID"];
                        string workplaceName = (string)reader["WorkplaceName"];
                        string workplaceAddress = (string)reader["WorkplaceAddress"];
                        string workplaceImagePath = (string)reader["WorkPlaceImagePath"];
                        int storageID = (int)reader["StorageID"];
                        _workplaces.Add(new Workplace(workplaceID, workplaceName, workplaceAddress, workplaceImagePath, storageID));
                    }
                }
            }
        }

        //Insert Workplace Into Database
        public void CreateWorkplaceInDatabase(string workplaceName, string workplaceAddress, string workplaceImagePath, int storageID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                //Use a stored procedure to prevent an sql injection
                string query = RepoCreateQuery;

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@WorkplaceName", workplaceName);
                    command.Parameters.AddWithValue("@WorkplaceAddress", workplaceAddress);
                    command.Parameters.AddWithValue("WorkplaceImagePath", workplaceImagePath);
                    command.Parameters.AddWithValue("@StorageID", storageID);
                    command.ExecuteNonQuery();
                }
            }
        }

        //Read Workplace From Database
        //Update Workplace In Database
        //Delete Workplace From Database
    }
}

