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

        //burde måske tjekkes i BLL, men ja shit happens
        public static Bil OpretBil(Bil bil)
        {
            using (FaergeContext context = new FaergeContext())
            {
                // Map the Bil object and add it to the context
                var mappedBil = BilMapper.Map(bil);
                context.Bil.Add(mappedBil);

                // Get the associated Booking and Afrejse
                var booking = context.Booking.FirstOrDefault(b => b.id == mappedBil.bookingId);
                var afrejse = context.Afrejse.FirstOrDefault(a => a.afrejseId == booking.afrejseId);

                // Check if the associated Faerge has reached its car limit
                var faergeId = afrejse.faergeid;
                var currentCarCount = context.Bil.Count(b => b.booking.afrejse.faergeid == faergeId);

                var faerge = context.Faerge.FirstOrDefault(f => f.id == faergeId);

                if (faerge != null && currentCarCount >= faerge.maxAntalBiler)
                {
                    // Handle the case where the car limit is reached, for example, throw an exception
                    throw new InvalidOperationException("The ferry has reached its car limit.");
                }

                context.SaveChanges();
            }

            return bil;
        }

        //public static Bil OpretBil(Bil bil)
        //{
        //    using (FaergeContext context = new FaergeContext())
        //    {
        //        context.Bil.Add(BilMapper.Map(bil));
        //        context.SaveChanges();
        //    }
        //    return bil;
        //}



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
        public static async Task<Bil> DeleteBil(string id)
        {
            using (FaergeContext context = new FaergeContext())
            {
                Model.Bil res = context.Bil
                    .FirstOrDefault(f => f.nummerplade == id);
                if (res != null)
                {
                    context.Bil.Remove(res);
                    await context.SaveChangesAsync();

                }
                return null;

            }

        }

    }
}
