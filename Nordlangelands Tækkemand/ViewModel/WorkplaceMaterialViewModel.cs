using Nordlangelands_Tækkemand.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordlangelands_Tækkemand.ViewModel
{
    public class WorkplaceMaterialViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        // INotifyPropertyChanged Method
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //Fields
        public WorkplaceMaterialsRepository WorkplaceMaterialRepo;
        private int _materialID;
        private string _materialName;
        private string _materialDescription;
        private int _workplaceMaterialStockCount;
        private int _storageID;
        private int _workplaceID;

        //Properties
        public int MaterialID
        {
            get { return _materialID; }
            set { _materialID = value; }
        }

        public string MaterialName
        {
            get { return _materialName; }
            set { _materialName = value; }
        }

        public string MaterialDescription
        {
            get { return _materialDescription; }
            set { _materialDescription = value; }
        }

        public int WorkplaceMaterialStockCount
        {
            get { return _workplaceMaterialStockCount; }
            set
            {
                if (_workplaceMaterialStockCount != value)
                {
                    _workplaceMaterialStockCount = value;
                    OnPropertyChanged(nameof(WorkplaceMaterialStockCount));

                }
            }
        }

        public int StorageID
        {
            get { return _storageID; }
            set { _storageID = value; }
        }

        public int WorkplaceID
        {
            get { return _workplaceID; }
            set { _workplaceID = value; }
        }   

        //Constructor
        public WorkplaceMaterialViewModel( WorkplaceMaterial workplaceMaterial)         
        {
            MaterialID = workplaceMaterial.MaterialID;
            MaterialName = workplaceMaterial.MaterialName;
            MaterialDescription = workplaceMaterial.MaterialDescription;
            WorkplaceMaterialStockCount = workplaceMaterial.WorkplaceMaterialStockCount;
            StorageID = workplaceMaterial.StorageID;
            WorkplaceID = workplaceMaterial.WorkplaceID;
            WorkplaceMaterialRepo = new WorkplaceMaterialsRepository();
        }

        //Methods
        public void ClearMaterialsInRepo()
        {
            WorkplaceMaterialRepo.ClearMaterialsInRepo();
        }
        
        public void InitializeWorkplaceMaterialsByWorkplaceID(int workplaceID)
        {
            WorkplaceMaterialRepo.InitializeWorkplaceMaterialsByWorkplaceID(workplaceID);
        }

    }
}
