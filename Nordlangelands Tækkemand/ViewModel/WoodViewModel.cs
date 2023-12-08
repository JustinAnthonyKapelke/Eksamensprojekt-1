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
    public class WoodViewModel : INotifyPropertyChanged, IMaterialViewModel
    {
        public WoodRepository woodRepo;

        //Auto-Implemented Properties  
        public int MaterialID { get; set; }
        public string MaterialName { get; set; }
        public string MaterialDescription { get; set; }
        public string MaterialImagePath { get; set; }
        
        public string MaterialType { get; set; }
        public int MaterialTypeID { get; set; }
        public int StorageID { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        // INotifyPropertyChanged Method
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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
        public WoodViewModel(WoodMaterial material)
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

            woodRepo = new WoodRepository(CreateDelegate, InitializeDelegate);
        }

        //Constructor Overlload
        public WoodViewModel()
        {
            woodRepo = new WoodRepository(CreateDelegate, InitializeDelegate);
        }

        public WoodMaterial CreateDelegate(string materialName, string materialDescription, string materialImagePath, int materialStockCount, int materialTypeID, int storageID)
        {
            return new WoodMaterial(materialName, materialDescription, materialImagePath, materialStockCount, materialTypeID, storageID);
        }

        public WoodMaterial InitializeDelegate(int materialID, string materialName, string materialDescription, string materialImagePath, int materialStockCount, string materialType, int storageID)
        {
            return new WoodMaterial(materialID, materialName, materialDescription, materialImagePath, materialStockCount, materialType, storageID);
        }

        //Method
        public void CreateMaterial(string materialName, string materialDescription, int materialStockCount, int materialTypeID, int storageID)
        {
            woodRepo.CreateMaterialInDatabase(materialName, materialDescription, materialStockCount, materialTypeID, storageID);
            woodRepo.InitializeMaterials();
        }

        public void DeleteMaterial(int materialID)
        {
            woodRepo.DeleteMaterialFromDatabase(materialID);
            woodRepo.InitializeMaterials();
        }
    }
}

