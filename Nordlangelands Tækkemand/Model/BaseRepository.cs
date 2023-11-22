using Nordlangelands_Tækkemand.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Nordlangelands_Tækkemand.Model
{
    public abstract class BaseRepository<T> where T : IMaterial
    {
        //Database Connection String
        protected string _connectionString = "Server=10.56.8.36; Database=DB_F23_TEAM_06; User Id=DB_F23_TEAM_06; Password=TEAMDB_DB_06; TrustServerCertificate=true";

        //Signature Delegate
        public delegate T CreateMaterialDelegate<T>(int materialID, string materialName, string materialDescription, int materialStorageIndex, double materialPrice);

        //Specific Delegate
        private readonly CreateMaterialDelegate<T> _createDelegate;

        //Material List Field
        protected List<T> _materials = new List<T>();

        //Material List Property
        public List<T> GetAll()
        {
            return _materials;
        }

        //Material Repository Constructor
        public BaseRepository(CreateMaterialDelegate<T> createDelegate)
        {
            _createDelegate = createDelegate; 
        }

        //CRUD METHODS
        public T CreateMaterial(int materialID, string materialName, string materialDescription, int materialStorageIndex, double materialPrice)
        {
            T createdMaterial = _createDelegate(materialID, materialName, materialDescription, materialStorageIndex, materialPrice);
            _materials.Add(createdMaterial);
            return createdMaterial;
        }
                           
        public T ReadMaterial(int materialID)
        {
            return _materials.FirstOrDefault(m => m.MaterialID == materialID);
        }

        public void UpdateMaterial(int materialID, string newMaterialName, string newMaterialDescription, int newMaterialStorageIndex, double newMaterialPrice)
        {
            T updatedMaterial = _materials.FirstOrDefault(m => m.MaterialID == materialID);

            if (updatedMaterial != null) 
            {
                updatedMaterial.MaterialName = newMaterialName;
                updatedMaterial.MaterialDescription = newMaterialDescription;
                updatedMaterial.MaterialStorageIndex = newMaterialStorageIndex;
                updatedMaterial.MaterialPrice = newMaterialPrice;
            }
        }

        public void DeleteMaterial(int materialID)
        {
            T deletedMaterial = _materials.FirstOrDefault(m => m.MaterialID == materialID);

            if (deletedMaterial != null)
            {
                _materials.Remove(deletedMaterial);
            }
        }
    }
}
