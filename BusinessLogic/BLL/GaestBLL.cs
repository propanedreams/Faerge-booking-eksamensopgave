using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Model;
using DataAccess.Repos;

namespace BusinessLogic.BLL
{
    public class GaestBLL
    {
        public Gaest getGaest(int id)
        {
            return DataAccess.Repos.GaestRepos.getGuest(id);
        }

        public List<Gaest> getAllGaest()
        {
            return DataAccess.Repos.GaestRepos.getAllGaest();
        }

        public Gaest OpreGaest(Gaest gaest)
        {
            return DataAccess.Repos.GaestRepos.OpreGaest(gaest);
        }
    }
}
