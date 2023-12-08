using DataAccess.Context;
using DataAccess.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Model;
using Microsoft.EntityFrameworkCore;
using DTO.Model.ModelSimpel;


namespace DataAccess.Repos
{
    public class AfrejseRepos
    {
        public static Afrejse getAfrejse(int id)
        {
            using (FaergeContext context = new FaergeContext())
            {
                //Maybe throw exception if not found
                return AfrejseMapper.Map(context.Afrejse.Where(a => a.afrejseId == id).Include(a => a.faerge).First());
            }
        }


        public static List<Afrejse> getAllAfrejse()
        {
            List<Afrejse> list = new List<Afrejse>();
            using (FaergeContext context = new FaergeContext())
            {
                foreach (DataAccess.Model.Afrejse item in context.Afrejse.Include(x => x.faerge))
                {
                    list.Add(AfrejseMapper.Map(item));
                }
            }
            return list;
        }

        //post
        public static async Task<Afrejse> OpretAfrejseAsync(Afrejse afrejse)
        {
            using (FaergeContext context = new FaergeContext())
            {
                // Validate or perform additional operations if necessary
                //Faerge f = FaergeMapper.Map(context.Faerge.Where(a => a.id == afrejse.faergeid).FirstOrDefault());
                //afrejse.faergeid = f.id;
                context.Afrejse.Add(AfrejseMapper.Map(afrejse));
                 context.SaveChanges();

                return afrejse;
            }
        }

        public static List<DTO.Model.Faerge> GetAfrejseFaerge(int id)
        {

            List<DTO.Model.Faerge> res = new();
            using (FaergeContext context = new FaergeContext())
            {
                foreach(var x in context.Faerge.Where(f => f.id == id).Include(f => f.id))
                {
                    res.Add(FaergeMapper.Map(x));
                }
            }
            return res;
        }

        //liste af bookings på rejse
        //liste af bookings på rejse
        public static List<DTO.Model.Booking> GetBookingForAfgang(int id)
        {
            List<DTO.Model.Booking> res = new();
            using (FaergeContext context = new FaergeContext())
            {
                foreach (var x in context.Booking.Where(a => a.afrejseId == id).Include(a=> a.afrejse))
                {
                    res.Add(BookingMapper.Map(x));
                }
            }
            return res;
        }

        public static List<DTO.Model.Afrejse> GetBookingForAfgang1(int id)
        {
            List<DTO.Model.Afrejse> res = new();
            using (FaergeContext context = new FaergeContext())
            {
                foreach (var x in context.Afrejse.Where(a => a.afrejseId == id))
                {
                    res.Add(AfrejseMapper.Map(x));
                }
            }
            return res;
        }


        public static  Afrejse OpdaterAfrejse(Afrejse af)
        {
            using (FaergeContext context = new FaergeContext())
            {
                // Retrieve the existing from the database
                DataAccess.Model.Afrejse existing =  context.Afrejse.Find(af.afrejseId);

                // Update the properties of the existing
                existing.afrejseId = af.afrejseId;
                existing.dato = af.dato;

                // Save changes to the database
                 context.SaveChanges();

                return AfrejseMapper.MapS(existing);
            }

        }


        public static async Task<Afrejse> DeleteAfrejse(int id)
        {
            using (FaergeContext context = new FaergeContext())
            {
                Model.Afrejse res = context.Afrejse
                    .FirstOrDefault(f => f.afrejseId == id);
                if (res != null)
                {
                    context.Afrejse.Remove(res);
                    await context.SaveChangesAsync();

                }
                return null;

            }

        }
    }
}
