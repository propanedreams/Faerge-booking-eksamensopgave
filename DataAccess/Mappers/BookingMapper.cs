using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using DTO.Model.ModelSimpel;

namespace DataAccess.Mappers
{
    public class BookingMapper
    {
        public static DTO.Model.Booking Map(Booking booking)
        {
            //skal fixes her
            DTO.Model.Booking b = new DTO.Model.Booking(booking.id, booking.dato, booking.afrejseId);
            b.afrejse = AfrejseMapper.MapToSimpelX(booking.afrejse);
            return b;
        }

        public static Booking Map(DTO.Model.Booking booking)
        {
            //skal fixes her
            return new Booking(booking.id, booking.dato, booking.afrejseId);
           
           
        }

        public static List<DTO.Model.Booking> Map(List<Booking> booking)
        {
            List<DTO.Model.Booking> retur = new List<DTO.Model.Booking>();
            foreach (Booking item in booking)
            {
                DTO.Model.Booking b = new DTO.Model.Booking(item.id, item.dato, item.afrejseId);
                b.afrejse = AfrejseMapper.MapToSimpelX(item.afrejse);


                retur.Add(b);
            }
            return retur;
        }

        public static BookingSimpel SimpleMap(Booking booking)
        {
            return new BookingSimpel
            {
                id = booking.id,
                dato = booking.dato,
                afrejseId = booking.afrejseId

            };
        }
        

    }
}
