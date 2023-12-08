using Microsoft.Identity.Client.Extensions.Msal;
using Nordlangelands_Tækkemand.Commands;
using Nordlangelands_Tækkemand.Interfaces;
using Nordlangelands_Tækkemand.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Media3D;


namespace Nordlangelands_Tækkemand.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {


        //public CreateMaterialWindow CreateMaterialWindow { get; set; }

        //Command Properties
        public ICommand SearchMaterialCMD { get; set; } = new SearchMaterialCommand();
        public ICommand CreateMaterialCMD { get; set; } = new CreateMaterialCommand();
        public ICommand FilterMaterialCMD { get; set; } = new FilterMaterialCommand();
        public ICommand AddStockCountCMD { get; set; } = new AddStockCountCommand();
        public ICommand RemoveStockCountCMD { get; set; } = new RemoveStockCountCommand();
        public ICommand DeleteMaterialCMD { get; set; } = new DeleteMaterialCommand();
        public ICommand UpdateMaterialCMD { get; set; } = new DeleteMaterialCommand();

        public ICommand UpdateRadioButtonsCMD { get; set; } = new UpdateRadioButtons();

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
        private ObservableCollection<IMaterialViewModel> _allMaterialsVM;

        // Current ViewModel object to store the selected viewmodel. It is used to change datacontext for the materialListbox
        private object _currentVM;

        public object CurrentVM
        {
            get { return _currentVM; }
            set
            {
                if (_currentVM != value)
                {
                    _currentVM = value;
                    OnPropertyChanged(nameof(CurrentVM));
                }
            }
        }

        //Selected material in listbox

        private IMaterialViewModel _selectedMaterial;

        public IMaterialViewModel SelectedMaterial
        {
            get { return _selectedMaterial; }
            set
            {
                if (_selectedMaterial != value)
                {
                    _selectedMaterial = value;
                    OnPropertyChanged("SelectedMaterial");
                }
            }
        }

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
            set
            {
                _variousVM = value;
                OnPropertyChanged(nameof(VariousVM));
            }
        }

        public ObservableCollection<WoodViewModel> WoodVM
        {
            get { return _woodVM; }
            set
            {
                _woodVM = value;
                OnPropertyChanged(nameof(WoodVM));
            }
        }

        public ObservableCollection<IMaterialViewModel> AllMaterialsVM
        {
            get { return _allMaterialsVM; }
            set
            {
                _allMaterialsVM = value;
                OnPropertyChanged(nameof(AllMaterialsVM));
            }
        }

        public ObservableCollection<WorkplaceViewModel> WorkplaceVM
        {
            get { return _workplaceVM; }
            set { _workplaceVM = value; }
        }



        private MainWindow _mainWindowInstance;

        public MainWindow MainWindowInstance
        {
            get { return _mainWindowInstance; }
            set
            {
                if (_mainWindowInstance != value)
                {
                    _mainWindowInstance = value;
                    OnPropertyChanged(nameof(MainWindow));
                }
            }
        }
        //Constructor
        public MainViewModel(MainWindow mainWindow)
        {

            _mainWindowInstance = mainWindow;
            ThatchingVM = new ObservableCollection<ThatchingViewModel>();
            VariousVM = new ObservableCollection<VariousViewModel>();
            WoodVM = new ObservableCollection<WoodViewModel>();
            WorkplaceVM = new ObservableCollection<WorkplaceViewModel>();
            WorkplaceVM = new ObservableCollection<WorkplaceViewModel>();
            AllMaterialsVM = new ObservableCollection<IMaterialViewModel>();

            // Initialize ThatchingViewModel
            TVM = new ThatchingViewModel(new ThatchingMaterial());
            VVM = new VariousViewModel(new VariousMaterial());
            WDVM = new WoodViewModel(new WoodMaterial());

            // Initialize ThatchingVM
            InitializeThatchingVM();
            InitializeVariousVM();
            InitializeWoodVM();
            InitializeAllMaterialsVM();
            //GetTypeFromRadioButton();

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

        public void InitializeVariousVM()
        {
            List<VariousMaterial> variousMaterials = VVM.variousRepo.GetAllMaterials();

            // Go through each of the materials from the repository and create a VariousViewModel for them
            foreach (VariousMaterial material in variousMaterials)
            {
                VariousVM.Add(new VariousViewModel(material));
            }
        }

        public void InitializeWoodVM()
        {
            List<WoodMaterial> woodMaterials = WDVM.woodRepo.GetAllMaterials();

            // Go through each of the materials from the repository and create a WoodViewModel for them
            foreach (WoodMaterial material in woodMaterials)
            {
                WoodVM.Add(new WoodViewModel(material));
            }
        }

        public void InitializeAllMaterialsVM()      
        {           
            // Add Thatching materials to the AllMaterials collection
            foreach (var thatchingViewModel in ThatchingVM)
            {
                AllMaterialsVM.Add(thatchingViewModel);
            }

            // Add Various materials to the AllMaterials collection
            foreach (var variousViewModel in VariousVM)
            {
                AllMaterialsVM.Add(variousViewModel);
            }

            // Add Wood materials to the AllMaterials collection
            foreach (var woodViewModel in WoodVM)
            {
                AllMaterialsVM.Add(woodViewModel);
            }

            // Add any other material categories in a similar fashion



        }

        private bool _isThatchingTypeChecked;

        public bool IsThatchingTypeChecked
        {
            get => _isThatchingTypeChecked;
            set
            {
                if (_isThatchingTypeChecked != value)
                {
                    _isThatchingTypeChecked = value;
                    OnPropertyChanged(nameof(IsThatchingTypeChecked)); // Ensure this calls PropertyChanged event
                }
            }
        }

        //public void GetTypeFromRadioButton()
        //{
        //    // Ensure that SelectedMaterial is not null before trying to access its properties
        //    if (SelectedMaterial != null)
        //    {
        //        //var selectedMaterialType = mvm.SelectedMaterial.MaterialType;

        //        // Update the ViewModel property instead of directly setting the RadioButton
        //        if (CurrentVM == ThatchingVM)
        //            MainWindowInstance.UpdateMaterialWindow.ThatchingTypeRadioButton.IsChecked = true;
        //    }
        //    else
        //    {
        //        // Reset the ViewModel property if SelectedMaterial is null
        //        MainWindowInstance.UpdateMaterialWindow.ThatchingTypeRadioButton.IsChecked = false;
        //    }
        //}
    }
}
