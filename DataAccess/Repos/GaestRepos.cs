using DataAccess.Context;
using DataAccess.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Model;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repos
{
    public class GaestRepos
    {
        public static Gaest getGuest(int id)
        {
            using (FaergeContext context = new FaergeContext())
            {
                //Maybe throw exception if not found
                return GaestMapper.Map(context.Gaest.Where(a => a.gaestId == id).Include(a => a.bil).First());
            }
        }


        public static List<Gaest> getAllGaest()
        {
            List<Gaest> list = new List<Gaest>();
            using (FaergeContext context = new FaergeContext())
            {
                foreach (DataAccess.Model.Gaest item in context.Gaest.Include(x => x.bil))
                {
                    list.Add(GaestMapper.Map(item));
                }
            }
            return list;
        }

        public static Gaest OpreGaest(Gaest gaest)
        {
            using (FaergeContext context = new FaergeContext())
            {
                context.Gaest.Add(GaestMapper.Map(gaest));
                context.SaveChanges();
            }
            return gaest;
        }
    }
}
