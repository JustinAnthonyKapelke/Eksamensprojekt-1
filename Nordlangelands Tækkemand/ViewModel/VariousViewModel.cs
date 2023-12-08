using Nordlangelands_Tækkemand.Interfaces;
using Nordlangelands_Tækkemand.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordlangelands_Tækkemand.ViewModel
{
    public class VariousViewModel : INotifyPropertyChanged, IMaterialViewModel
    {
        public VariousRepository variousRepo;

        // Property changed event
        public event PropertyChangedEventHandler? PropertyChanged;

        // INotifyPropertyChanged Method
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //Auto-Implemented Properties
        public int MaterialID { get; set; }
        public string MaterialName { get; set; }
        public string MaterialDescription { get; set; }
        public string MaterialImagePath { get; set; }      
        public string MaterialType { get; set; }
        public int MaterialTypeID { get; set; }
        public int StorageID { get; set; }


        private int _materialStockCount;
        public int MaterialStockCount
        {
            get { return _materialStockCount; }
            set
            {
                if (_materialStockCount != value)
                {
                    _materialStockCount = value;
                    OnPropertyChanged(nameof(MaterialStockCount));

                }
            }
        }

        //Constructor
        public VariousViewModel(VariousMaterial material)
        {
            // Initialize properties using the ThatchingMaterial object
            MaterialID = material.MaterialID;
            MaterialName = material.MaterialName;
            MaterialDescription = material.MaterialDescription;
            MaterialImagePath = material.MaterialImagePath;
            MaterialStockCount = material.MaterialStockCount;
            MaterialType = material.MaterialType;
            MaterialTypeID = material.MaterialTypeID;
            StorageID = material.StorageID;

            variousRepo = new VariousRepository(CreateDelegate, InitializeDelegate);
        }
       
        // Constructor Overload  
        public VariousViewModel()
        {
           variousRepo = new VariousRepository(CreateDelegate, InitializeDelegate);
        }
        
        public VariousMaterial CreateDelegate(string materialName, string materialDescription, string materialImagePath, int materialStockCount, int materialTypeID, int storageID)
        {
            return new VariousMaterial(materialName, materialDescription,materialImagePath, materialStockCount, materialTypeID, storageID);
        }

        public VariousMaterial InitializeDelegate(int materialID, string materialName, string materialDescription, string materialImagePath, int materialStockCount, string materialType, int storageID)
        {
            return new VariousMaterial(materialID, materialName, materialDescription,materialImagePath, materialStockCount, materialType, storageID);
        }

        //Method
        public void CreateMaterial(string materialName, string materialDescription, int materialStockCount, int materialTypeID, int storageID)
        {
            variousRepo.CreateMaterialInDatabase(materialName, materialDescription, materialStockCount, materialTypeID, storageID);
            variousRepo.InitializeMaterials();
        }
        public void DeleteMaterial(int materialID)
        {
            variousRepo.DeleteMaterialFromDatabase(materialID);
            variousRepo.InitializeMaterials();
        }
    }
}
