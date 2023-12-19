using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordlangelands_Tækkemand.Model
{
    public class User
    {
        //Properties
        public int UserID { get; private set; }
        public string UserName { get; private set; }    
        public string UserPassword { get; private set; }

        //Constructor
        public User() { }      

        //Constructor overload
        public User(int userID, string userName, string userPassword) 
        { 
            UserID= userID;
            UserName= userName;
            UserPassword= userPassword;
        }
        
    }
}
