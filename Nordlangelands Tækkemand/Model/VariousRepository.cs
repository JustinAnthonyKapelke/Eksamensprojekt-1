using Microsoft.Data.SqlClient;
using Nordlangelands_Tækkemand.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordlangelands_Tækkemand.Model
{
    //The class inherits the BaseRepository class
    public class VariousRepository : BaseRepository<VariousMaterial>
    {
        //Overwrite RepoQuery Inherited From BaseRepository
        protected override string RepoCreateQuery { get; set; } = "EXEC sp_NTReadVariousMaterial";
        protected override string RepoReadQuery { get; set; } = "EXEC sp_NTReadVariousMaterial";

        //Constructor
        public VariousRepository(CreateDelegate<VariousMaterial> createDelegate) : base(createDelegate)
        {
          
        }
    }
}
