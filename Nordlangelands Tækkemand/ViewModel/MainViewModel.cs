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
        public ICommand LoginCMD { get; set; } = new LoginCommand();
        public ICommand LogTextCMD { get; set; } = new LogTextCommand();
        public ICommand OpenCreateMaterialCMD { get; set; } = new OpenCreateMaterialWindowCommand();
        public ICommand OpenUpdateMaterialCMD { get; set; } = new OpenUpdateMaterialWindowCommand();
        public ICommand RemoveStockCountCMD { get; set; } = new RemoveStockCountCommand();
        public ICommand SearchMaterialCMD { get; set; } = new SearchMaterialCommand();
        public ICommand UpdateMaterialCMD { get; set; } = new UpdateMaterialCommand();     
        public ICommand OpenWorkplaceCMD { get; set; } = new OpenWorkplaceWindowCommand();
        public ICommand WorkplaceAddStockCountCMD { get; set; } = new WorkplaceAddStockCountCommand();


        //Fields  
        private bool _isThatchingChecked;
        private bool _isVariousChecked;
        private bool _isWoodChecked;
        private bool _storageIsAllChecked;
        private bool _storageIsThatchingChecked;
        private bool _storageIsVariousChecked;
        private bool _storageIsWoodChecked;
        private bool _storageTabIsEnabled;
        private bool _updateIsThatchingChecked;
        private bool _updateIsVariousChecked;
        private bool _updateIsWoodChecked;
        private bool _workplaceTabIsEnabled;
        private IMaterialViewModel _selectedMaterial;
        private int _createMaterialStorageID;
        private int _newStockCount;
        private int _selectedTabIndex;
        private object _currentVM;
        private string _createMaterialDescription;
        private string _createMaterialName;
        private string _createMaterialStockCount;
        private string _logText;
        private string _searchText;
        private string _selectedMaterialType;
        private string _userName;
        private string _userPassword;
        private WorkplaceMaterialViewModel _workplaceSelectedMaterial;
        private WorkplaceViewModel _selectedWorkplace;

        //ObservableCollection Fields
        private ObservableCollection<IMaterialViewModel> _allMaterialsVM;
        private ObservableCollection<ThatchingViewModel> _thatchingVM;
        private ObservableCollection<UserViewModel> _userVM;
        private ObservableCollection<VariousViewModel> _variousVM;
        private ObservableCollection<WoodViewModel> _woodVM;
        private ObservableCollection<WorkplaceMaterialViewModel> _workplaceMaterialsVM;
        private ObservableCollection<WorkplaceViewModel> _workplaceVM;

        //ViewModel Properties
        public ThatchingViewModel TVM { get; set; }
        public UserViewModel UVM { get; set; }
        public VariousViewModel VVM { get; set; }
        public WoodViewModel WDVM { get; set; }
        public WorkplaceMaterialViewModel WKMVM { get; set; }
        public WorkplaceViewModel WKVM { get; set; }

        //Properties
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

        public bool IsVariousChecked
        {
            get { return _isVariousChecked; }
            set
            {
                if (_isVariousChecked != value)
                {
                    _isVariousChecked = value;
                    OnPropertyChanged(nameof(IsVariousChecked));
                }
            }
        }

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

        public bool StorageTabIsEnabled
        {
            get { return _storageTabIsEnabled; }
            set
            {
                if (_storageTabIsEnabled != value)
                {
                    _storageTabIsEnabled = value;
                    OnPropertyChanged(nameof(StorageTabIsEnabled)); 
                }
            }
        }
       
        public bool StorageIsVariousChecked
        {
            get { return _storageIsVariousChecked; }
            set
            {
                if (_storageIsVariousChecked != value)
                {
                    _storageIsVariousChecked = value;
                    OnPropertyChanged(nameof(StorageIsVariousChecked));
                }
            }
        }

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
        
        public bool WorkplaceTabIsEnabled
        {
            get { return _workplaceTabIsEnabled; }
            set
            {
                if (_workplaceTabIsEnabled != value)
                {
                    _workplaceTabIsEnabled = value;
                    OnPropertyChanged(nameof(WorkplaceTabIsEnabled));
                }
            }
        }

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
        
        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set
            {
                _selectedTabIndex = value;
                OnPropertyChanged(nameof(SelectedTabIndex));
            }
        }

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
        
        public string CreateMaterialName
        {
            get { return _createMaterialName; }
            set
            {
                if (_createMaterialName != value)
                {
                    _createMaterialName = value;
                    OnPropertyChanged(nameof(CreateMaterialName));
                }
            }
        }

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
        
        public string LogText
        {
            get { return _logText; }
            set
            {
                if (_logText != value)
                {
                    _logText = value;
                    OnPropertyChanged(nameof(LogText)); 
                }
            }
        }      

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged(nameof(SearchText)); 

                }
            }
        }

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

        //Observable Collection Properties       
        public ObservableCollection<IMaterialViewModel> AllMaterialsVM
        {
            get { return _allMaterialsVM; }
            set
            {
                _allMaterialsVM = value;
                OnPropertyChanged(nameof(AllMaterialsVM));
            }
        }

        public ObservableCollection<ThatchingViewModel> ThatchingVM
        {
            get { return _thatchingVM; }
            set
            {
                _thatchingVM = value;
                OnPropertyChanged(nameof(ThatchingVM));
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

            //Set default value of newStockCount
            _newStockCount = 1;
        }
        
        //Methods
        public void InitializeAllMaterialsVM()
        {
            // Add ThatchingMaterials to AllMaterials Collection
            foreach (var thatchingViewModel in ThatchingVM)
            {
                AllMaterialsVM.Add(thatchingViewModel);
            }

            // Add VariousMaterials to AllMaterials Collection
            foreach (var variousViewModel in VariousVM)
            {
                AllMaterialsVM.Add(variousViewModel);
            }

            // Add WoodMaterials to AllMaterials Collection
            foreach (var woodViewModel in WoodVM)
            {
                AllMaterialsVM.Add(woodViewModel);
            }
        }

        public void InitializeThatchingVM()
        {  
            List<ThatchingMaterial> thatchingMaterials = TVM.thatchingRepo.GetAllMaterials();

            //For each Material in the Repository, create a ViewModel and add to the Collection
            foreach (ThatchingMaterial material in thatchingMaterials)
            {
                ThatchingVM.Add(new ThatchingViewModel(material));
            }
        }

        public void InitializeUserVM()
        {
            List<User> users = UVM.UserRepo.GetAllUsers();

            //For each User in the Repository, create a ViewModel and add to the Collection
            foreach (User user in users)
            {
                UserVM.Add(new UserViewModel(user));
            }
        }

        public void InitializeVariousVM()
        {
            List<VariousMaterial> variousMaterials = VVM.variousRepo.GetAllMaterials();

            //For each Material in the Repository, create a ViewModel and add to the Collection
            foreach (VariousMaterial material in variousMaterials)
            {
                VariousVM.Add(new VariousViewModel(material));
            }
        }

        public void InitializeWoodVM()
        {
            List<WoodMaterial> woodMaterials = WDVM.woodRepo.GetAllMaterials();

            //For each Material in the Repository, create a ViewModel and add to the Collection
            foreach (WoodMaterial material in woodMaterials)
            {
                WoodVM.Add(new WoodViewModel(material));
            }
        }

        public void InitializeWorkplaceMaterialsVM()
        {      
            List<WorkplaceMaterial> workplaceMaterials = WKMVM.WorkplaceMaterialRepo.GetAll();

            //For each WorkplaceMaterial in the Repository, create a ViewModel and add to the Collection
            foreach (WorkplaceMaterial workplaceMaterial in workplaceMaterials)
            {
                WorkplaceMaterialsVM.Add(new WorkplaceMaterialViewModel(workplaceMaterial));
            }
        }
       
        public void InitializeWorkplaceVM()
        {
            List<Workplace> workplaces = WKVM.workplaceRepo.GetAllWorkplaces();

            //For each Workplace in the Repository, create a ViewModel and add to the Collection
            foreach (Workplace workplace in workplaces)
            {
                WorkplaceVM.Add(new WorkplaceViewModel(workplace));
            }
        }
    }
}
