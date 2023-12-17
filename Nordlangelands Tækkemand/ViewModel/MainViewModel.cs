using Microsoft.Identity.Client.Extensions.Msal;
using Nordlangelands_Tækkemand.Commands;
using Nordlangelands_Tækkemand.Commands.MainCommands;
using Nordlangelands_Tækkemand.Commands.StorageCommands;
using Nordlangelands_Tækkemand.Commands.WorkplaceCommands;
using Nordlangelands_Tækkemand.Interfaces;
using Nordlangelands_Tækkemand.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace Nordlangelands_Tækkemand.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        // INotifyPropertyChanged EventHandler
        public event PropertyChangedEventHandler? PropertyChanged;

        // INotifyPropertyChanged Method
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //Command Properties
        public ICommand AddStockCountCMD { get; set; } = new AddStockCountCommand();
        public ICommand CreateMaterialCMD { get; set; } = new CreateMaterialCommand();
        public ICommand DeleteMaterialCMD { get; set; } = new DeleteMaterialCommand();
        public ICommand FilterMaterialCMD { get; set; } = new FilterMaterialCommand();
        public ICommand LogTextCMD { get; set; } = new LogTextCommand();
        public ICommand OpenCreateMaterialCMD { get; set; } = new OpenCreateMaterialWindowCommand();
        public ICommand OpenUpdateMaterialCMD { get; set; } = new OpenUpdateMaterialWindowCommand();
        public ICommand OpenWorkplaceCMD { get; set; } = new OpenWorkplaceWindowCommand();
        public ICommand RemoveStockCountCMD { get; set; } = new RemoveStockCountCommand();
        public ICommand SearchMaterialCMD { get; set; } = new SearchMaterialCommand();
        public ICommand UpdateMaterialCMD { get; set; } = new UpdateMaterialCommand();
        public ICommand WorkplaceAddStockCountCMD { get; set; } = new WorkplaceAddStockCountCommand();
        public ICommand LoginCMD { get; set; } = new LoginCommand();

        //Fields
  
        private string _searchText;
        private string _logText;
        private object _currentVM;
        private IMaterialViewModel _selectedMaterial;
        private WorkplaceViewModel _selectedWorkplace;
        private string _selectedMaterialType;
        private MainWindow _mainWindowInstance;
        private WorkplaceMaterialViewModel _workplaceSelectedMaterial;
        private string _userName;
        private string _userPassword;
        private int _newStockCount;



        //Constructor
        public MainViewModel()
        {
           //Instantiate ObservableCollections        
            ThatchingVM = new ObservableCollection<ThatchingViewModel>();
            VariousVM = new ObservableCollection<VariousViewModel>();
            WoodVM = new ObservableCollection<WoodViewModel>();
            WorkplaceVM = new ObservableCollection<WorkplaceViewModel>();
            AllMaterialsVM = new ObservableCollection<IMaterialViewModel>();
            WorkplaceMaterialsVM = new ObservableCollection<WorkplaceMaterialViewModel>();
            UserVM = new ObservableCollection<UserViewModel>();

            //Instantiate ViewModels
            TVM = new ThatchingViewModel(new ThatchingMaterial());
            VVM = new VariousViewModel(new VariousMaterial());
            WDVM = new WoodViewModel(new WoodMaterial());
            WKVM = new WorkplaceViewModel(new Workplace());
            WKMVM = new WorkplaceMaterialViewModel(new WorkplaceMaterial());
            UVM = new UserViewModel(new User());

            //Initialize ViewModels
            ThatchingVM.Clear();
            InitializeThatchingVM();
            VariousVM.Clear();
            InitializeVariousVM();
            WoodVM.Clear();
            InitializeWoodVM();
            AllMaterialsVM.Clear();
            InitializeAllMaterialsVM();          
            InitializeWorkplaceVM();
            InitializeUserVM();

            //Set the default value of new stock count
            _newStockCount = 1;
        }

        //MainWindow
        private bool _storageTabIsEnabled;
        public bool StorageTabIsEnabled
        {
            get { return _storageTabIsEnabled; }
            set
            {
                if (_storageTabIsEnabled != value)
                {
                    _storageTabIsEnabled = value;
                    OnPropertyChanged(nameof(StorageTabIsEnabled)); // Implement INotifyPropertyChanged
                }
            }
        }

        private bool _workplaceTabIsEnabled;
        public bool WorkplaceTabIsEnabled
        {
            get { return _workplaceTabIsEnabled; }
            set
            {
                if (_workplaceTabIsEnabled != value)
                {
                    _workplaceTabIsEnabled = value;
                    OnPropertyChanged(nameof(WorkplaceTabIsEnabled)); // Implement INotifyPropertyChanged
                }
            }
        }

       //Selected tab index
        private int _selectedTabIndex;
        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set
            {
                _selectedTabIndex = value;
                OnPropertyChanged(nameof(SelectedTabIndex)); // Assuming INotifyPropertyChanged implementation
            }
        }

        //LogText Property
        public string LogText
        {
            get { return _logText; }
            set
            {
                if (_logText != value)
                {
                    _logText = value;
                    OnPropertyChanged(nameof(LogText)); // Implement INotifyPropertyChanged
                }
            }
        }      

        //SearchText Property
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

        //Selected Material Property
        public IMaterialViewModel SelectedMaterial
        {
            get { return _selectedMaterial; }
            set
            {
                if (_selectedMaterial != value )
                {
                    _selectedMaterial = value;
                    OnPropertyChanged("SelectedMaterial");
                }
            }
        }


        //Selected WorkplaceMaterial Property
        public WorkplaceMaterialViewModel WorkplaceSelectedMaterial
        {
            get { return _workplaceSelectedMaterial; }
            set
            {
                if (_workplaceSelectedMaterial != value)
                {
                    _workplaceSelectedMaterial = value;
                    OnPropertyChanged("WorkplaceSelectedMaterial");
                }
            }
        }

        //SelectedMaterialType Property
        public string SelectedMaterialType
        {
            get { return _selectedMaterialType; }
            set
            {
                if (_selectedMaterialType != value)
                {
                    _selectedMaterialType = value;
                    OnPropertyChanged(nameof(SelectedMaterialType));                    
                }
            }
        }

        //SelectedWorkplace Property
        public WorkplaceViewModel SelectedWorkplace
        {
            get { return _selectedWorkplace; }
            set
            {
                if (_selectedWorkplace != value)
                {
                    _selectedWorkplace = value;
                    OnPropertyChanged("SelectedWorkplace");
                }
            }
        }

        // Current ViewModel object to store the selected viewmodel. It is used to change datacontext for the materialListbox
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


        //UserName Property      

        public string UserName
        {
            get { return _userName; }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged(nameof(UserName));
                }
            }
        }

        // User password property
        public string UserPassword
        {
            get { return _userPassword; }
            set
            {
                if (_userPassword != value)
                {
                    _userPassword = value;
                    OnPropertyChanged(nameof(UserPassword));
                }
            }
        }

        // New stockcount property
        public int NewStockCount 
        {
            get { return _newStockCount; } 
            set
            {
                if (_newStockCount != value)
                {
                    _newStockCount = value;
                    OnPropertyChanged(nameof(NewStockCount));
                }
            }
        }       

        //Stock Tab properties
        private bool _storageIsThatchingChecked;
        public bool StorageIsThatchingChecked
        {
            get { return _storageIsThatchingChecked; }
            set
            {
                if (_storageIsThatchingChecked != value)
                {
                    _storageIsThatchingChecked = value;
                    OnPropertyChanged(nameof(StorageIsThatchingChecked));
                }
            }
        }
       
        private bool _storageIsWoodChecked;
        public bool StorageIsWoodChecked
        {
            get { return _storageIsWoodChecked; }
            set
            {
                if (_storageIsWoodChecked != value)
                {
                    _storageIsWoodChecked = value;
                    OnPropertyChanged(nameof(StorageIsWoodChecked));
                }
            }
        }


        private bool _storageIsvariousChecked;
        public bool StorageIsVariousChecked
        {
            get { return _storageIsvariousChecked; }
            set
            {
                if (_storageIsvariousChecked != value)
                {
                    _storageIsvariousChecked = value;
                    OnPropertyChanged(nameof(StorageIsVariousChecked));
                }
            }
        }

        private bool _storageIsAllChecked;
        public bool StorageIsAllChecked
        {
            get { return _storageIsAllChecked; }
            set
            {
                if (_storageIsAllChecked != value)
                {
                    _storageIsAllChecked = value;
                    OnPropertyChanged(nameof(StorageIsAllChecked));
                }
            }
        }


        //Create material window properties            


        private string _createMaterialName;
        public string CreateMaterialName
        {
            get { return _createMaterialName; }
            set
            {
                if (_createMaterialName != value)
                {
                    _createMaterialName = value;
                    OnPropertyChanged(nameof(_mainWindowInstance));
                }
            }
        }

        private string _createMaterialDescription;
        public string CreateMaterialDescription
        {
            get { return _createMaterialDescription; }
            set
            {
                if (_createMaterialDescription != value)
                {
                    _createMaterialDescription = value;
                    OnPropertyChanged(nameof(CreateMaterialDescription));
                }
            }
        }


        private string _createMaterialStockCount;
        public string CreateMaterialStockCount
        {
            get { return _createMaterialStockCount; }
            set
            {
                if (_createMaterialStockCount != value)
                {
                    _createMaterialStockCount = value;
                    OnPropertyChanged(nameof(CreateMaterialStockCount));
                }
            }
        }

        private int _createMaterialStorageID;
        public int CreateMaterialStorageID
        {
            get { return _createMaterialStorageID; }
            set
            {
                if (_createMaterialStorageID != value)
                {
                    _createMaterialStorageID = value;
                    OnPropertyChanged(nameof(CreateMaterialStorageID));
                }
            }
        }

        private bool _isThatchingChecked;
        public bool IsThatchingChecked
        {
            get { return _isThatchingChecked; }
            set
            {
                if (_isThatchingChecked != value)
                {
                    _isThatchingChecked = value;
                    OnPropertyChanged(nameof(IsThatchingChecked));
                }
            }
        }

        private bool _isWoodChecked;
        public bool IsWoodChecked
        {
            get { return _isWoodChecked; }
            set
            {
                if (_isWoodChecked != value)
                {
                    _isWoodChecked = value;
                    OnPropertyChanged(nameof(IsWoodChecked));
                }
            }
        }


        private bool _isvariousChecked;
        public bool IsVariousChecked
        {
            get { return _isvariousChecked; }
            set
            {
                if (_isvariousChecked != value)
                {
                    _isvariousChecked = value;
                    OnPropertyChanged(nameof(IsVariousChecked));
                }
            }
        }

        //Update material window properties
        private bool _updateIsThatchingChecked;
        public bool UpdateIsThatchingChecked
        {
            get { return _updateIsThatchingChecked; }
            set
            {
                if (_updateIsThatchingChecked != value)
                {
                    _updateIsThatchingChecked = value;
                    OnPropertyChanged(nameof(UpdateIsThatchingChecked));
                }
            }
        }

        private bool _updateIsWoodChecked;
        public bool UpdateIsWoodChecked
        {
            get { return _updateIsWoodChecked; }
            set
            {
                if (_updateIsWoodChecked != value)
                {
                    _updateIsWoodChecked = value;
                    OnPropertyChanged(nameof(UpdateIsWoodChecked));
                }
            }
        }

        private bool _updateIsVariousChecked;
        public bool UpdateIsVariousChecked
        {
            get { return _updateIsVariousChecked; }
            set
            {
                if (_updateIsVariousChecked != value)
                {
                    _updateIsVariousChecked = value;
                    OnPropertyChanged(nameof(UpdateIsVariousChecked));
                }
            }
        }



        //ViewModel Properties
        public ThatchingViewModel TVM { get; set; }
        public VariousViewModel VVM { get; set; }
        public WoodViewModel WDVM { get; set; }
        public WorkplaceViewModel WKVM { get; set; }
        public WorkplaceMaterialViewModel WKMVM { get; set; }
        public UserViewModel UVM { get; set; }


        //ObservableCollection Fields
        private ObservableCollection<ThatchingViewModel> _thatchingVM;
        private ObservableCollection<VariousViewModel> _variousVM;
        private ObservableCollection<WoodViewModel> _woodVM;
        private ObservableCollection<WorkplaceViewModel> _workplaceVM;
        private ObservableCollection<IMaterialViewModel> _allMaterialsVM;
        private ObservableCollection<WorkplaceMaterialViewModel> _workplaceMaterialsVM;
        private ObservableCollection<UserViewModel> _userVM;

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

        public ObservableCollection<WorkplaceMaterialViewModel> WorkplaceMaterialsVM
        {
            get { return _workplaceMaterialsVM; }
            set
            {
                _workplaceMaterialsVM = value;
                OnPropertyChanged(nameof(WorkplaceMaterialsVM));
            }
        }

        public ObservableCollection<WorkplaceViewModel> WorkplaceVM
        {
            get { return _workplaceVM; }
            set
            {
                _workplaceVM = value;
                OnPropertyChanged(nameof(WorkplaceVM));
            }
        }

        public ObservableCollection<UserViewModel> UserVM
        {
            get { return _userVM; }
            set
            {
                _userVM = value;
                OnPropertyChanged(nameof(UserVM));
            }
        }


        //Methods
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
        }
       
        public void InitializeWorkplaceVM()
        {
            List<Workplace> workplaces = WKVM.workplaceRepo.GetAllWorkplaces();

            // Go through each of the materials from the repository and create a VariousViewModel for them
            foreach (Workplace workplace in workplaces)
            {
                WorkplaceVM.Add(new WorkplaceViewModel(workplace));
            }
        }

        public void InitializeWorkplaceMaterialsVM()
        {      
            List<WorkplaceMaterial> workplaceMaterials = WKMVM.WorkplaceMaterialRepo.GetAll();

            // Go through each of the materials from the repository and create a WoodViewModel for them
            foreach (WorkplaceMaterial workplaceMaterial in workplaceMaterials)
            {
                WorkplaceMaterialsVM.Add(new WorkplaceMaterialViewModel(workplaceMaterial));
            }
        }

        public void InitializeUserVM()
        {
            List<User> users = UVM.UserRepo.GetAllUsers();

            // Go through each of the materials from the repository and create a VariousViewModel for them
            foreach (User user in users)
            {
                UserVM.Add(new UserViewModel(user));
            }
        }
    }
}
