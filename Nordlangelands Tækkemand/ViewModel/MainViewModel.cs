using Nordlangelands_Tækkemand.Commands;
using Nordlangelands_Tækkemand.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Nordlangelands_Tækkemand.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {

        //Command Properties
        public ICommand SearchMaterialCMD { get; set; } = new SearchMaterialCommand();
        public ICommand CreateMaterialCMD { get; set; } = new CreateMaterialCommand();

        // INotifyPropertyChanged EventHandler
        public event PropertyChangedEventHandler? PropertyChanged;

        // INotifyPropertyChanged Method
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //Search Property
        private string _searchText;

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged(nameof(SearchText)); // Implement INotifyPropertyChanged
                    
                }
            }
        }

        //ViewModel Properties
        public ThatchingViewModel TVM { get; set; }
        public VariousViewModel VVM { get; set; }
        public WoodViewModel WDVM { get; set; }
        public WorkplaceViewModel WKVM { get; set; }

        //ObservableCollection Fields
        private ObservableCollection<ThatchingViewModel> _thatchingVM;
        private ObservableCollection<VariousViewModel> _variousVM;
        private ObservableCollection<WoodViewModel> _woodVM;
        private ObservableCollection<WorkplaceViewModel> _workplaceVM;



        //Observable Collection Properties       
        public ObservableCollection<ThatchingViewModel> ThatchingVM
        {
            get { return _thatchingVM; }
            set
            {
                _thatchingVM = value;
                OnPropertyChanged(nameof(ThatchingVM));
            }
        }

        public ObservableCollection<VariousViewModel> VariousVM
        {
            get { return _variousVM; }
            set { _variousVM = value; }
        }

        public ObservableCollection<WoodViewModel> WoodVM
        {
            get { return _woodVM; }
            set { _woodVM = value; }
        }

        public ObservableCollection<WorkplaceViewModel> WorkplaceVM
        {
            get { return _workplaceVM; }
            set { _workplaceVM = value; }
        }

        //Constructor
        public MainViewModel()
        {

            ThatchingVM = new ObservableCollection<ThatchingViewModel>();
            VariousVM = new ObservableCollection<VariousViewModel>();
            WoodVM = new ObservableCollection<WoodViewModel>();
            WorkplaceVM = new ObservableCollection<WorkplaceViewModel>();

            // Initialize ThatchingViewModel
            TVM = new ThatchingViewModel(new ThatchingMaterial());

            // Initialize ThatchingVM
            InitializeThatchingVM();
        }

        // Get the list of thatching materials from the repository

        public void InitializeThatchingVM()
        {
            List<ThatchingMaterial> thatchingMaterials = TVM.thatchingRepo.GetAllMaterials();

            // Go through each of the materials from the repository and create a ThatchingViewModel for them
            foreach (ThatchingMaterial material in thatchingMaterials)
            {
                ThatchingVM.Add(new ThatchingViewModel(material));
            }
        }
    }
}
