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



        //burde måske tjekkes i BLL, men ja shit happens

        public static Gaest OpretGaest(Gaest gaest)
        {
            using (FaergeContext context = new FaergeContext())
            {
                // Map the Bil object and add it to the context
                var x = GaestMapper.Map(gaest);
                context.Gaest.Add(x);


                // Get the associated Booking and Afrejse
                var bil = context.Bil.FirstOrDefault(b => b.nummerplade == x.bilId);
                var booking = context.Booking.FirstOrDefault(b => b.id == bil.bookingId);
                var afrejse = context.Afrejse.FirstOrDefault(a => a.afrejseId == booking.afrejseId);

                // Check if the associated Faerge has reached its car limit
                var faergeId = afrejse.faergeid;
                var currentGaestCount = context.Gaest.Count(g => g.bil.booking.afrejse.faergeid == faergeId);

                var faerge = context.Faerge.FirstOrDefault(f => f.id == faergeId);

                if (faerge != null && currentGaestCount >= faerge.maxAntalGaester)
                {
                    // Handle the case where the car limit is reached, for example, throw an exception
                    throw new InvalidOperationException("The ferry has reached its gaest limit.");
                }

                context.SaveChanges();
            }

            return gaest;
        }

        //public static Gaest OpreGaest(Gaest gaest)
        //{
        //    using (FaergeContext context = new FaergeContext())
        //    {
        //        context.Gaest.Add(GaestMapper.Map(gaest));
        //        context.SaveChanges();
        //    }
        //    return gaest;
        //}

        public static async Task<Gaest> DeleteGaest(int id)
        {
            using (FaergeContext context = new FaergeContext())
            {
                Model.Gaest res = context.Gaest
                    .FirstOrDefault(f => f.gaestId == id);
                if (res != null)
                {
                    context.Gaest.Remove(res);
                    await context.SaveChangesAsync();

                }
                return null;

            }

        }

    }
}
