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
        // Property changed event
        public event PropertyChangedEventHandler? PropertyChanged;

        // INotifyPropertyChanged Method
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        //Fields
        public VariousRepository variousRepo;
        private int _materialID;
        private string _materialName;
        private string _materialDescription;
        private string _materialType;
        private string _materialImagePath;
        private int _materialStockCount;
        private int _materialTypeID;
        private int _storageID;

        //Properties
        public int MaterialID
        {
            get { return _materialID; }
            set
            {
                if (_materialID != value)
                {
                    _materialID = value;
                    OnPropertyChanged(nameof(MaterialID));

                }
            }
        }

        public string MaterialName
        {
            get { return _materialName; }
            set
            {
                if (_materialName != value)
                {
                    _materialName = value;
                    OnPropertyChanged(nameof(MaterialName));

                }
            }
        }

        public string MaterialDescription
        {
            get { return _materialDescription; }
            set
            {
                if (_materialDescription != value)
                {
                    _materialDescription = value;
                    OnPropertyChanged(nameof(MaterialDescription));

                }
            }
        }

        public string MaterialType
        {
            get { return _materialType; }
            set
            {
                if (_materialType != value)
                {
                    _materialType = value;
                    OnPropertyChanged(nameof(MaterialType));

                }
            }
        }

        public string MaterialImagePath
        {
            get { return _materialImagePath; }
            set
            {
                if (_materialImagePath != value)
                {
                    _materialImagePath = value;
                    OnPropertyChanged(nameof(MaterialImagePath));

                }
            }
        }

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

        public int MaterialTypeID
        {
            get { return _materialTypeID; }
            set
            {
                if (_materialTypeID != value)
                {
                    _materialTypeID = value;
                    OnPropertyChanged(nameof(MaterialTypeID));

                }
            }
        }


        public int StorageID
        {
            get { return _storageID; }
            set
            {
                if (_storageID != value)
                {
                    _storageID = value;
                    OnPropertyChanged(nameof(StorageID));

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
       
        //Constructor Overload  
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

        //Methods
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
