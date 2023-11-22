using Nordlangelands_Tækkemand.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordlangelands_Tækkemand.Interfaces
{
    //Polymorph
    public interface IRepository<T> where T : class
    {
        //List
        List<T> GetAll();

        //CRUD Methods
        T Create(int materialID, string materialName, string materialDescription, int materialStorageIndex, double materialPrice);
        T Read(int materialID);
        void Update(int materialID);
        void Delete(int materialID);
         
        //Initialize Method
        void Initialize();
    } 
}
