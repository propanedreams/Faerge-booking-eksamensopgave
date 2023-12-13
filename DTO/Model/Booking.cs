using DTO.Model.ModelSimpel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Model
{
    public class Booking
    {
        public Booking() { }
        public Booking(DateTime dato)
        {
            this.dato = dato;
        }
        public Booking(int bookingId, DateTime dato, int afrejseid)
        {
            this.id = bookingId;
            this.dato = dato;
            this.afrejseId = afrejseid;
        }
        
        public int id { get; set; }
        
        public DateTime dato { get; set; }
        public int afrejseId { get; set; }
        public AfrejseSimpel afrejse { get; set; }

        public override string ToString()
        {
            return id.ToString();
        }
    }

}

