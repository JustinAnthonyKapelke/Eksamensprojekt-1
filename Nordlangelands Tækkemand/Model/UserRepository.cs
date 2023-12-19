using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordlangelands_Tækkemand.Model
{
    public class UserRepository
    {
        //Database Connection String
        private string _connectionString = "Server=10.56.8.36; Database=DB_F23_TEAM_06; User Id=DB_F23_TEAM_06; Password=TEAMDB_DB_06; TrustServerCertificate=true";

        private string InitializeQuery { get; set; } = "Select * From NTLoginInformation";
        //List Of Users
        private List<User> _users;

        //Constructor
        public UserRepository()
        {
            _users = new List<User>();
            InitializeUsers();
        }

        //Methods
        public List<User> GetAllUsers()
        {
            return _users;
        }
        
        public void InitializeUsers()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                //Use a stored procedure to prevent sql injection.     
                using (SqlCommand command = new SqlCommand(InitializeQuery, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int userID = (int)reader["UserID"];
                        string userName = (string)reader["UserName"];
                        string userPassword = (string)reader["UserPassword"];

                        User newUser = new User(userID, userName, userPassword);

                        _users.Add(newUser);
                    }
                }
            }
        }
    }
}
