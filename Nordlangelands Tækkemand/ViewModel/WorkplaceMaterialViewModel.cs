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
     public WorkplaceMaterialsRepository WorkplaceMaterialRepo;



        private int _materialID;

        public int MaterialID
        {
            get { return _materialID; }
            set { _materialID = value; }
        }



        private string _materialName;

        public string MaterialName
        {
            get { return _materialName; }
            set { _materialName = value; }
        }


     

        private string _materialDescription;

        public string MaterialDescription
        {
            get { return _materialDescription; }
            set { _materialDescription = value; }
        }




        private int _workplaceMaterialStockCount;
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

        private int _storageID;

        public int StorageID
        {
            get { return _storageID; }
            set { _storageID = value; }
        }

        private int _workplaceID;


        public int WorkplaceID
        {
            get { return _workplaceID; }
            set { _workplaceID = value; }
        }   
       
        public event PropertyChangedEventHandler? PropertyChanged;


        // INotifyPropertyChanged Method
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



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
    }
}
