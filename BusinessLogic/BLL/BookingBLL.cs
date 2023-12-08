using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Model;

namespace BusinessLogic.BLL
{
    public class BookingBLL
    {
        public Booking getBooking(int id)
        {
            return DataAccess.Repos.BookingRepos.getBooking(id);
        }

        public List<Booking> getAllBooking()
        {
            return DataAccess.Repos.BookingRepos.getAllBooking();
        }

        public List<Bil> GetBilerOnBooking(int id)
        {
            return DataAccess.Repos.BookingRepos.GetBilerOnBooking(id);
        }

        public List<Booking> getAfrejseBookings(int id)
        {
            return DataAccess.Repos.AfrejseRepos.GetBookingForAfgang(id);
        }
        public Booking OpretBooking(Booking x)
        {
            return DataAccess.Repos.BookingRepos.OpretBooking(x);
        }

    }
}

