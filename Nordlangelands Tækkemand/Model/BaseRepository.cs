using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client.Extensions.Msal;
using Nordlangelands_Tækkemand.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation.Peers;
using System.Windows.Media.Media3D;

namespace Nordlangelands_Tækkemand.Model
{
    //Inheritance
    public abstract class BaseRepository<T> where T : IMaterial
    {
        //Database Connection String
        protected string _connectionString = "Server=10.56.8.36; Database=DB_F23_TEAM_06; User Id=DB_F23_TEAM_06; Password=TEAMDB_DB_06; TrustServerCertificate=true";

        //Specific Delegates
        private readonly CreateDelegate<T> _createDelegate;
        private readonly InitializeCreateDelegate<T> _initializeCreateDelegate;

        //Signature Delegate
        public delegate T CreateDelegate<T>(string materialName, string materialDescription, string materialImagePath, int materialStockCount, int materialTypeID, int storageID);

        //Signature Delegate For Initalize
        public delegate T InitializeCreateDelegate<T>(int materialID, string materialName, string materialDescription, string materialImagePath, int materialStockCount, int materialTypeID, int storageID);

        //Material List
        protected List<T> _materials = new List<T>();

        //Database Queries
        protected virtual string RepoInitializeQuery { get; set; } = "";
        protected virtual string RepoCreateQuery { get; set; } = "";
        protected virtual string RepoReadQuery { get; set; } = "";
        protected virtual string RepoUpdateQuery { get; set; } = "";
        protected virtual string RepoDeleteQuery { get; set; } = "";

        //Constructor
        public BaseRepository(CreateDelegate<T> createDelegate)
        {
            _createDelegate = createDelegate;
            InitializeMaterials();
        }

        //Constructor overload
        public BaseRepository(CreateDelegate<T> createDelegate, InitializeCreateDelegate<T> initializeCreateDelegate)
        {
            _createDelegate = createDelegate;
            _initializeCreateDelegate = initializeCreateDelegate;
            InitializeMaterials();
        }

        //CRUD Methods

        public T CreateMaterialInRepository(string materialName, string materialDescription, string materialImagePath, int materialStockCount, int materialTypeID, int storageID)
        {
            T newMaterial = _createDelegate(materialName, materialDescription, materialImagePath, materialStockCount, materialTypeID, storageID);
            _materials.Add(newMaterial);
            return newMaterial;
        }

        public T ReadMaterialFromRepisitory(int materialID)
        {
            return _materials.FirstOrDefault(m => m.MaterialID == materialID);
        }

        public void UpdateMaterialInRepository(int materialID, string newMaterialName, string newMaterialDescription, int newMaterialStockCount, int newMaterialTypeID, int newStorageID)
        {
            T updatedMaterial = _materials.FirstOrDefault(m => m.MaterialID == materialID);

            if (updatedMaterial != null)
            {
                updatedMaterial.MaterialName = newMaterialName;
                updatedMaterial.MaterialDescription = newMaterialDescription;
                updatedMaterial.MaterialStockCount = newMaterialStockCount;
                updatedMaterial.MaterialTypeID = newMaterialTypeID;
                updatedMaterial.StorageID = newStorageID;
            }
        }

        public void DeleteMaterialFromRepisitory(int materialID)
        {
            T deletedMaterial = _materials.FirstOrDefault(m => m.MaterialID == materialID);

            if (deletedMaterial != null)
            {
                _materials.Remove(deletedMaterial);
            }
        }

        //Database Operation Methods

        //Initialize Materials From Database
        public void InitializeMaterials()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                //Use a stored procedure to prevent sql injection.
                string query = RepoInitializeQuery;

                using (SqlCommand command = new SqlCommand(query, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int materialID = (int)reader["MaterialID"];
                        string materialName = (string)reader["MaterialName"];
                        string materialDescription = (string)reader["MaterialDescription"];
                        string materialImagePath = (string)reader["MaterialImagePath"];
                        int materialStockCount = (int)reader["MaterialStockCount"];
                        int materialTypeID = (int)reader["MaterialTypeID"];
                        int storageID = (int)reader["StorageID"];


                        T newMaterial = _initializeCreateDelegate(materialID, materialName, materialDescription, materialImagePath, materialStockCount, materialTypeID, storageID);

                        _materials.Add(newMaterial);
                    }
                }
            }
        }

        //Create Material In Database
        public void CreateMaterialInDatabase(string materialName, string materialDescription,string materialImagePath,int materialTypeID, int storageID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                //Use a stored procedure to prevent an sql injection
                string query = RepoCreateQuery;
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaterialName", materialName);
                    command.Parameters.AddWithValue("@MaterialDescription", materialDescription);
                    command.Parameters.AddWithValue("@MaterialImagePath", materialImagePath);                   
                    command.Parameters.AddWithValue("@MaterialTypeID", materialTypeID);
                    command.Parameters.AddWithValue("@StorageID", storageID);
                    command.ExecuteNonQuery();
                }
            }
        }

        //Read Last Added Material From Database
        public T ReadLastAddedMaterialFromDatabase()
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
                        int materialID = (int)reader["MaterialID"];
                        string materialName = (string)reader["MaterialName"];
                        string materialDescription = (string)reader["MaterialDescription"];
                        string materialImagePath = (string)reader["MaterialImagePath"];
                        int materialStockCount = (int)reader["MaterialStockCount"];
                        int materialTypeID = (int)reader["MaterialTypeID"];
                        int storageID = (int)reader["StorageID"];

                        T newMaterial = _initializeCreateDelegate(materialID, materialName, materialDescription, materialImagePath, materialStockCount, materialTypeID, storageID);

                        return newMaterial;
                    }
                }
            }
            // Handle the case where no material is found
            return default(T);
        }

        //Update Material In Database

        //Delete Material From Database
        public void DeleteMaterialFromDatabase(int materialID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                //Use a stored procedure to prevent an sql injection
                string query = RepoDeleteQuery;
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MaterialID", materialID);

                    command.ExecuteNonQuery();
                }
            }
        }

        //Get all materials method
        public List<T> GetAllMaterials()
        {
            return _materials.ToList();
        }

    }

}
