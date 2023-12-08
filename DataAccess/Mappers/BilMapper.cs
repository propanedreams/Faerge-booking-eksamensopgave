using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using DTO.Model.ModelSimpel;

namespace DataAccess.Mappers
{
    public class BilMapper
    {
        public static DTO.Model.Bil Map(Bil bil)
        {
            return new DTO.Model.Bil
            {
                nummerplade = bil.nummerplade,
                model = bil.model,
                længde = bil.længde,
                bookingid = bil.bookingId,
                booking = BookingMapper.SimpleMap(bil.booking)
            };
        }

        public static Bil Map(DTO.Model.Bil bil)
        {
            return new Bil
            {
                nummerplade = bil.nummerplade,
                model = bil.model,
                længde = bil.længde,
                bookingId = bil.bookingid,
               
            };
        }


        //simplemapper
        public static BilSimpel MapToSimpel(Bil bil)
        {
            return new BilSimpel
            {
                nummerplade = bil.nummerplade,
                model = bil.model,
                længde = bil.længde,
                bookingid   = bil.bookingId
            };
        }



    }
}
