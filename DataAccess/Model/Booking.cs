using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Booking
    {
        public Booking() { }
        public Booking( DateTime dato) {
            this.dato = dato;
        }
        public Booking(int bookingId, DateTime dato) { 
        this.id = bookingId;
            this.dato = dato;
        }

        public Booking(int bookingId, DateTime dato, int afrejseid)
        {
            this.id = bookingId;
            this.dato = dato;
            this.afrejseId = afrejseid;
        }

        [Key]
        public int id { get; set; }
        public DateTime dato { get; set; }
        public int afrejseId { get; set; }
        public Afrejse afrejse { get; set; }
    }

}
