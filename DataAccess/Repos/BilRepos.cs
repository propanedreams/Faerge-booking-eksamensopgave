using DataAccess.Context;
using DataAccess.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Model;

namespace DataAccess.Repos
{
    public class BilRepos
    {
        public static Bil getBil(string id)
        {
            using (FaergeContext context = new FaergeContext())
            {
                //Maybe throw exception if not found
                return BilMapper.Map(context.Bil.Where(a => a.nummerplade == id).Include(a => a.booking).First());
            }
        }


        public static List<Bil> getAllBil()
        {
            List<Bil> list = new List<Bil>();
            using (FaergeContext context = new FaergeContext())
            {
                foreach (DataAccess.Model.Bil item in context.Bil.Include(x=> x.booking))
                {
                    list.Add(BilMapper.Map(item));
                }
            }
            return list;
        }
        public static Bil OpretBil(Bil bil)
        {
            using (FaergeContext context = new FaergeContext())
            {
                context.Bil.Add(BilMapper.Map(bil));
                context.SaveChanges();
            }
            return bil;
        }

        public static List<DTO.Model.Gaest> GetGeasterOnBil(string id)
        {

            List<DTO.Model.Gaest> res = new();
            using (FaergeContext context = new FaergeContext())
            {
                foreach (var x in context.Gaest.Where(b => b.bilId == id).Include(b => b.bil))
                {
                    res.Add(GaestMapper.Map(x));
                }
            }
            return res;
        }

    }
}
