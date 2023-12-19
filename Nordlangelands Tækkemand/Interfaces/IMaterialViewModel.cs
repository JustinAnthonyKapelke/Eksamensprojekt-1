using Nordlangelands_Tækkemand.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordlangelands_Tækkemand.Interfaces
{
    public interface IMaterialViewModel<T> where T : IMaterial
    {
        //Properties
        public int MaterialID { get; set; }
        public string MaterialName { get; set; }
        public string MaterialDescription { get; set; }
        public string MaterialImagePath { get; set; }
        public string MaterialType { get; set; }
        public int MaterialTypeID { get; set; }
        public int MaterialStockCount { get; set; }
        public int StorageID { get; set; }

        // Generic Delegates        
        Func<string, string, string, int, int, int, T> CreateMaterial { get; }
        Func<int, string, string, string, int, string, int, T> InitializeMaterial { get; }


        // Metoder til databaseinteraktion
        T ReadLastAddedMaterialFromDatabase();
        T ReadMaterialByIDFromDatabase(int materialID);
        string ReadLogTextFromDatabase();
        void CreateMaterialInDatabase(T material);
        void DeleteMaterialFromDatabaseByID(int materialID);
        void InitializeMaterials();
        void UpdateMaterial(T material);
        void UpdateStockCountInDatabase(int materialID, int newMaterialAmount);
    }
}


