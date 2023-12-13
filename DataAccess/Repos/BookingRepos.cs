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
    public class BookingRepos
    {
        public static Booking getBooking(int id)
        {

            using (FaergeContext context = new FaergeContext())
            {
                //Maybe throw exception if not found
                return BookingMapper.Map(context.Booking.Where(b => b.id == id).Include(b => b.afrejse).First());
            }
        }
        //includer afrejse objektet som sendes videre
        public static List<Booking> getAllBooking()
        {
            List<Booking> list = new List<Booking>();
            using (FaergeContext context = new FaergeContext())
            {
                foreach (DataAccess.Model.Booking item in context.Booking.Include(b => b.afrejse))
                {
                    list.Add(BookingMapper.Map(item));
                }
            }
            return list;
        }
        public static List<DTO.Model.Afrejse> GetBookingForAfgang(int id)
        {
            List<DTO.Model.Afrejse> res = new();
            using (FaergeContext context = new FaergeContext())
            {
                foreach (var x in context.Afrejse.Where(a => a.afrejseId == id).Include(b => b.afrejseId))
                {
                    res.Add(AfrejseMapper.Map(x));
                }
            }
            return res;
        }

        public static Booking OpretBooking(Booking booking)
        {
            using (FaergeContext context = new FaergeContext())
            {
                context.Booking.Add(BookingMapper.Map(booking));
                context.SaveChanges();
            }
            return booking;
        }
        public static List<DTO.Model.Bil> GetBilerOnBooking(int id)
        {

            List<DTO.Model.Bil> res = new();
            using (FaergeContext context = new FaergeContext())
            {
                foreach (var x in context.Bil.Where(b => b.bookingId == id).Include(b => b.booking))
                {
                    res.Add(BilMapper.Map(x));
                }
            }
            return res;
        }

        public static async Task<Booking> DeleteBooking(int id)
        {
            using (FaergeContext context = new FaergeContext())
            {
                Model.Booking res = context.Booking
                    .FirstOrDefault(f => f.id == id);
                if (res != null)
                {
                    context.Booking.Remove(res);
                    await context.SaveChangesAsync();

                }
                return null;

            }

        }

    }
}


