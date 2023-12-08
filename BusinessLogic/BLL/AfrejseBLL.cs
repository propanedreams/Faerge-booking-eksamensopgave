using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repos;
using DTO.Model;
using DTO.Model.ModelSimpel;

namespace BusinessLogic.BLL
{
    public class AfrejseBLL
    {
        public Afrejse getAfrejse(int id)
        {
            return DataAccess.Repos.AfrejseRepos.getAfrejse(id);
        }


        public List<Booking> getAfrejseBookings(int id)
        {
            return DataAccess.Repos.AfrejseRepos.GetBookingForAfgang(id);
        }

        public List<Afrejse> getAllAfrejse()
        {
            return DataAccess.Repos.AfrejseRepos.getAllAfrejse();
        }

        public async  Task<Afrejse> OpretAfrejseAsync(Afrejse Afrejse)
        {
            return await DataAccess.Repos.AfrejseRepos.OpretAfrejseAsync(Afrejse);
           
        }

        public Afrejse OpdaterAfrejse(Afrejse afrejse)
        {
            return AfrejseRepos.OpdaterAfrejse(afrejse);

        }

        public async Task<Afrejse> DeleteAfrejse(int id)
        {
            return  await AfrejseRepos.DeleteAfrejse(id);

        }

    }
}
