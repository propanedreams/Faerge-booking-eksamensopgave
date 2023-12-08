using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Model;
using DataAccess.Repos;

namespace BusinessLogic.BLL
{
    public class BilBLL
    {
        public Bil getBil(String id)
        {
            return DataAccess.Repos.BilRepos.getBil(id);
        }

        public List<Bil> getAllBil()
        {
            return DataAccess.Repos.BilRepos.getAllBil();
        }
        public Bil OpretBil(Bil x)
        {
            return DataAccess.Repos.BilRepos.OpretBil(x);
        }
        public List<Gaest> GetGaesterOnBil(string id)
        {
            return DataAccess.Repos.BilRepos.GetGeasterOnBil(id);
        }


    }
}
