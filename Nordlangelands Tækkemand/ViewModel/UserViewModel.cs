using Nordlangelands_Tækkemand.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordlangelands_Tækkemand.ViewModel
{
    public class UserViewModel : INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler? PropertyChanged;

        // INotifyPropertyChanged Method
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //Fields
        public UserRepository UserRepo;
        private int _userID;
        private string _userName;
        private string _userPassword;

        //Properties
        public int UserID
        {
            get { return _userID; }
            set
            {
                if (_userID != value)
                {
                    _userID = value;
                    OnPropertyChanged(nameof(UserID));

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

        public UserViewModel(User user)
        {
            UserID = user.UserID;
            UserName = user.UserName;
            UserPassword = user.UserPassword;
            UserRepo = new UserRepository();
        }


        ////Methods
        //public List<User> GetAllUsers()
        //{
        //    return UserRepo.GetAllUsers();
        //}

    }
}
