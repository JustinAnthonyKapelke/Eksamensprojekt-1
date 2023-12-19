using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client.Extensions.Msal;
using Nordlangelands_Tækkemand.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
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
        private readonly InitializeDelegate<T> _initializeDelegate;

        //Signature Delegate Create
        public delegate T CreateDelegate<T>(string materialName, string materialDescription, string materialImagePath, int materialStockCount, int materialTypeID, int storageID);

        //Signature Delegate Initialize
        public delegate T InitializeDelegate<T>(int materialID, string materialName, string materialDescription, string materialImagePath, int materialStockCount, string materialType, int storageID);

        //Database Queries
        protected string InitializeQuery { get; set; } = "SELECT * FROM NTCombinedMaterialView";
        protected string ReadLogTextQuery { get; set; } = "SELECT TOP 1 LogText FROM NTLog ORDER BY LogID DESC";
        protected string CreateQuery { get; set; } = "EXEC sp_NTCreateMaterial @MaterialName, @MaterialDescription, @MaterialStockCount, @MaterialTypeID, @StorageID;";
        protected string ReadLastAddedQuery { get; set; } = "EXEC sp_NTReadLastMaterial";
        protected string ReadQuery { get; set; } = "EXEC sp_NTGetMaterialByID @MaterialID";
        protected string UpdateStockCountQuery { get; set; } = "EXEC sp_NTUpdateStockCount @MaterialID, @NewMaterialAmount";
        protected string UpdateQuery { get; set; } = "EXEC sp_NTUpdateMaterial @MaterialID, @MaterialName, @MaterialDescription, @MaterialStockCount, @MaterialTypeID, @StorageID";
        protected virtual string DeleteQuery { get; set; } = "EXEC sp_NTDeleteMaterial @MaterialID";

        //Fields
        protected virtual string MaterialType { get; set; } = "";
        private readonly object _lock = new object();
        protected List<T> _materials = new List<T>();

        //Constructor
        public BaseRepository(CreateDelegate<T> createDelegate)
        {
            _createDelegate = createDelegate;           

            //Seperate thread to initialize materials
            Thread initializeThread = new Thread(InitializeMaterials);
            initializeThread.Start();
        }

        //Constructor overload
        public BaseRepository(CreateDelegate<T> createDelegate, InitializeDelegate<T> initializeCreateDelegate)
        {
            _createDelegate = createDelegate;
            _initializeDelegate = initializeCreateDelegate;

            Thread thread = new(InitializeMaterials);
            thread.Start();
            thread.Join();

            //// Initialize and start the worker thread
            //_workerThread = new Thread(Work);
            //_workerThread.Start();

            //// Enqueue material initialization task
            //EnqueueTask(InitializeMaterials);
        }

        //Forsøg på tråde

        //private readonly Thread _workerThread;
        //private readonly ConcurrentQueue<Action> _taskQueue = new ConcurrentQueue<Action>();
        //private readonly AutoResetEvent _signal = new AutoResetEvent(false);
        //private bool _running = true;

        //private void Work()
        //{
        //    while (_running)
        //    {
        //        if (_taskQueue.TryDequeue(out Action task))
        //        {
        //            task.Invoke();
        //        }
        //        else
        //        {
        //            _signal.WaitOne(); // Wait for a signal to check the queue again
        //        }
        //    }
        //}

        //protected void EnqueueTask(Action task)
        //{
        //    _taskQueue.Enqueue(task);
        //    _signal.Set(); // Signal the worker thread
        //}

        //public void Stop()
        //{
        //    _running = false;
        //    _signal.Set(); // Signal to stop the thread
        //    _workerThread.Join(); // Optionally wait for the thread to finish
        //}

        //Methods  
        public List<T> GetAllMaterials()
        {
            lock (_lock)
            {
                return _materials.ToList();
            }
        }
        
        public string ReadLogTextFromDatabase()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
           
                using (SqlCommand command = new SqlCommand(ReadLogTextQuery, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())                   
                    {
                        string logText = (string)reader["LogText"];                        
                        return logText;
                    }
                }
            }
            return default(string);
        }

        public T ReadLastAddedMaterialFromDatabase()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
   
                using (SqlCommand command = new SqlCommand(ReadLastAddedQuery, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int materialID = (int)reader["MaterialID"];
                        string materialName = (string)reader["MaterialName"];
                        string materialDescription = (string)reader["MaterialDescription"];
                        int materialStockCount = (int)reader["MaterialStockCount"];
                        string materialType = (string)reader["MaterialType"];
                        int storageID = (int)reader["StorageID"];
                        string materialImagePath = (string)reader["MaterialImagePath"];

                        T newMaterial = _initializeDelegate(materialID, materialName, materialDescription, materialImagePath, materialStockCount, materialType, storageID);

                        return newMaterial;
                    }
                }
            }
            return default(T);
        }  
        
        public T ReadMaterialByIDFromDatabase(int materialID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
          
                using (SqlCommand command = new SqlCommand(ReadQuery, connection))
                {
                    command.Parameters.AddWithValue("@MaterialID", materialID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string materialName = (string)reader["MaterialName"];
                            string materialDescription = (string)reader["MaterialDescription"];
                            string materialImagePath = (string)reader["MaterialImagePath"];
                            int materialStockCount = (int)reader["MaterialStockCount"];
                            string materialType = (string)reader["MaterialType"];                            
                            int storageID = (int)reader["StorageID"];

                            T newMaterial = _initializeDelegate(materialID, materialName, materialDescription, materialImagePath, materialStockCount, materialType, storageID);
                            return newMaterial;
                        }
                    }
                }
            }
            throw new InvalidOperationException("Material with ID " + materialID + " not found in the database.");
        }
        
        public void InitializeMaterials()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                lock (_lock)
                {
                    connection.Open();

                    string wantedMaterialType = MaterialType;
                    using (SqlCommand command = new SqlCommand(InitializeQuery, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int materialID = (int)reader["MaterialID"];
                            string materialName = (string)reader["MaterialName"];
                            string materialDescription = (string)reader["MaterialDescription"];
                            int materialStockCount = (int)reader["MaterialStockCount"];
                            string materialType = (string)reader["MaterialType"];
                            string materialImagePath = (string)reader["MaterialImagePath"];
                            int storageID = (int)reader["StorageID"];

                            if (wantedMaterialType == materialType)
                            {
                                T newMaterial = _initializeDelegate(materialID, materialName, materialDescription, materialImagePath, materialStockCount, materialType, storageID);

                                _materials.Add(newMaterial);
                            }
                        }
                    }
                }
            }
        }         

        public void ClearMaterialsInRepo()
        {
            lock (_lock)
            {
                _materials.Clear();
            }
        }
        
        public void CreateMaterialInDatabase(string materialName, string materialDescription, int materialStockCount, int materialTypeID, int storageID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(CreateQuery, connection))
                {
                    command.Parameters.AddWithValue("@MaterialName", materialName);
                    command.Parameters.AddWithValue("@MaterialDescription", materialDescription);                  
                    command.Parameters.AddWithValue("@MaterialStockCount", materialStockCount);
                    command.Parameters.AddWithValue("@MaterialTypeID", materialTypeID);
                    command.Parameters.AddWithValue("@StorageID", storageID);
                    command.ExecuteNonQuery();
                }
            }
        }      
        
        public void UpdateMaterialInDatabase(int materialID, string materialName, string materialDescription, int materialTypeID, int materialStockCount, int storageID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(UpdateQuery, connection))
                {
                    command.Parameters.AddWithValue("@MaterialID", materialID);
                    command.Parameters.AddWithValue("@MaterialName", materialName);
                    command.Parameters.AddWithValue("@MaterialDescription", materialDescription);                    
                    command.Parameters.AddWithValue("@MaterialTypeID", materialTypeID);
                    command.Parameters.AddWithValue("@MaterialStockCount", materialStockCount);
                    command.Parameters.AddWithValue("@StorageID", storageID);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateStockCountInDatabase(int materialID, int newMaterialAmount)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
               
                using (SqlCommand command = new SqlCommand(UpdateStockCountQuery, connection))
                {
                    command.Parameters.AddWithValue("@MaterialID", materialID);
                    command.Parameters.AddWithValue("@NewMaterialAmount", newMaterialAmount);
                    command.ExecuteNonQuery();
                }
            }
        }
        
        public void DeleteMaterialFromDatabaseByID(int materialID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
            
                using (SqlCommand command = new SqlCommand(DeleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@MaterialID", materialID);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
