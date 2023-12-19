using Nordlangelands_Tækkemand.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordlangelands_Tækkemand.ViewModel
{
    public class WorkplaceViewModel : INotifyPropertyChanged
    {
        // INotifyPropertyChanged Event
        public event PropertyChangedEventHandler? PropertyChanged;

        // INotifyPropertyChanged Method
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //Fields
        public WorkplaceRepository workplaceRepo;
        private int _workplaceID;
        private string _workplaceName;
        private string _workplaceAddress;
        private string _workplaceImagePath;
        private int _storageID;

        //Properties
        public int WorkplaceID
        {
            get { return _workplaceID; }
            set
            {
                if (_workplaceID != value)
                {
                    _workplaceID = value;
                    OnPropertyChanged(nameof(WorkplaceID));

                }
            }
        }
        public string WorkplaceName
        {
            get { return _workplaceName; }
            set
            {
                if (_workplaceName != value)
                {
                    _workplaceName = value;
                    OnPropertyChanged(nameof(WorkplaceName));

                }
            }
        }

        public string WorkplaceAddress
        {
            get { return _workplaceAddress; }
            set
            {
                if (_workplaceAddress != value)
                {
                    _workplaceAddress = value;
                    OnPropertyChanged(nameof(WorkplaceAddress));

                }
            }
        }

        public string WorkplaceImagePath
        {
            get { return _workplaceImagePath; }
            set
            {
                if (_workplaceImagePath != value)
                {
                    _workplaceImagePath = value;
                    OnPropertyChanged(nameof(WorkplaceImagePath));

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
        public WorkplaceViewModel(Workplace workplace)
        {
            // Initialize properties using the ThatchingMaterial object
            WorkplaceID = workplace.WorkplaceID;
            WorkplaceName = workplace.WorkplaceName;
            WorkplaceAddress = workplace.WorkplaceAddress;
            WorkplaceImagePath = workplace.WorkplaceImagePath;
            StorageID = workplace.StorageID;
            workplaceRepo = new WorkplaceRepository();
        }       
    }
}
