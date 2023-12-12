using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client.Extensions.Msal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nordlangelands_Tækkemand.Model;


namespace Nordlangelands_Tækkemand.Model
{
    public class WorkplaceRepository
    {
        //Database Connection String
        private string _connectionString = "Server=10.56.8.36; Database=DB_F23_TEAM_06; User Id=DB_F23_TEAM_06; Password=TEAMDB_DB_06; TrustServerCertificate=true";

        //List of workplaces
        private List<Workplace> _workplaces;

        //Database Queries jf. CRUD
       string RepoInitializeQuery { get; set; } = "SELECT * FROM NTWorkplace";
      string RepoCreateQuery { get; set; } = "EXEC sp_NTCreateWorkplace @WorkplaceName, @WorkplaceAddress, @WorkplaceImagePath, @StorageID";
        string RepoReadQuery { get; set; } = "EXEC sp_NTReadLastWorkplace";
        string RepoUpdateQuery { get; set; } = "EXEC sp_NTUpdateWorkplace @WorkplaceName, @WorkplaceAddress, @WorkplaceImagePath, @StorageID";
         string RepoDeleteQuery { get; set; } = "EXEC sp_NTDeleteWorkplace @WorkplaceID";

        string DatabaseUpdateQuery { get; set; } = "EXEC sp_NTCreateWorkplace @MorkplaceID, @WorkplaceName, @WorkplaceAddress, @WorkplaceImagePath, @StorageID";
        string ReadWorkplaceByIDQuery { get; set; } = "EXEC sp_NTGetWorkplaceByID @WorkplaceID";
        string ReadLogTextQuery { get; set; } = ""; // Tilføje her 

        //Constructor
        public WorkplaceRepository()
        {
            _workplaces = new List<Workplace>();
            InitializeWorkplaces();
        }

        public void InitializeWorkplaces()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                connection.Open();

                //Use a stored procedure to prevent sql injection.     
                using (SqlCommand command = new SqlCommand(RepoInitializeQuery, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int workplaceID = (int)reader["WorkplaceID"];
                        string workplaceName = (string)reader["WorkplaceName"];
                        string workplaceAddress = (string)reader["WorkplaceAddress"];
                        string workplaceImagePath = (string)reader["WorkplaceImagePath"];
                        int storageID = (int)reader["StorageID"];

                        
                        Workplace newWorkplace = new Workplace(workplaceID, workplaceName, workplaceAddress, workplaceImagePath, storageID);

                        _workplaces.Add(newWorkplace);
                        
                    }
                }
            }
        }

        //Create Workplace In Database
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
                    command.Parameters.AddWithValue("@WorkplaceImagePath", workplaceImagePath);
                    command.Parameters.AddWithValue("@StorageID", storageID);
                    command.ExecuteNonQuery();
                }
            }
        }

        //Update workplace in databse
        public void UpdateWorkplaceInDatabase(int workplaceID, string workplaceName, string workplaceAddress, string workplaceImagePath, int storageID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                //Use a stored procedure to prevent an sql injection
                string query = DatabaseUpdateQuery;
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@WorkplaceID", workplaceID);
                    command.Parameters.AddWithValue("@WorkplaceName", workplaceName);
                    command.Parameters.AddWithValue("@WorkplaceAddress", workplaceAddress);
                    command.Parameters.AddWithValue("@StorageID", storageID);
                    command.ExecuteNonQuery();
                }
            }
        }

        //Read Last Added Workplace From Database
        public Workplace ReadLastAddedWorkplaceFromDatabase()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Updated query to include all needed columns
                string query = RepoReadQuery;

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int workplaceID = (int)reader["WorkplaceID"];
                        string workplaceName = (string)reader["WorkplaceName"];
                        string workplaceAddress = (string)reader["WorkplaceAddress"];
                        string workplaceImagePath = (string)reader["WorkplaceImagePath"];
                        int storageID = (int)reader["StorageID"];


                        Workplace newWorkplace = new Workplace(workplaceID, workplaceName, workplaceAddress, workplaceImagePath, storageID);

                        return newWorkplace;
                    }
                }
            }
            // Handle the case where no material is found
            return default(Workplace);
        }

        //Update workplace In Database

        //Delete workplace From Database
        public void DeleteWorkplaceFromDatabase(int workplaceID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                //Use a stored procedure to prevent an sql injection
                string query = RepoDeleteQuery;
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@WorkplaceID", workplaceID);

                    command.ExecuteNonQuery();
                }
            }
        }

        //Get all materials method
        public List<Workplace> GetAllWorkplaces()
        {
            return _workplaces;
        }

        public void ClearWorkplacesInRepo()
        {
            _workplaces.Clear();
        }

        //Method to read workplace from database
        public Workplace ReadWorkplaceByIDFromDatabase(int workplaceID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Updated query to include all needed columns
                string query = ReadWorkplaceByIDQuery;

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Tilføj parameteren til kommandoen
                    command.Parameters.AddWithValue("@WorkplaceID", workplaceID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string workplaceName = (string)reader["WorkplaceName"];
                            string workplaceAddress = (string)reader["WorkplaceAddress"];
                            string workplaceImagePath = (string)reader["WorkplaceImagePath"];
                            int storageID = (int)reader["StorageID"];

                            Workplace newWorkplace = new Workplace(workplaceID, workplaceName, workplaceAddress, workplaceImagePath, storageID);

                            return newWorkplace;
                        }
                    }
                }
            }

            // If no workplace is found, throw an exception
            throw new InvalidOperationException("Workplace with ID " + workplaceID + " not found in the database.");
        }


        //Read Last Added workplace From Database
        public string ReadLogTextFromDatabase()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Updated query to include all needed columns
                string query = ReadLogTextQuery;

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        string logText = (string)reader["LogText"];
                        return logText;
                    }
                }
            }
            // Handle the case where no workplace is found
            return default(string);
        }

      
    }
}

