using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Model.ModelSimpel
{
    public class BookingSimpel
    {
        public BookingSimpel() { }
        public BookingSimpel(DateTime dato)
        {
            this.dato = dato;
        }
        public BookingSimpel(int bookingId, DateTime dato, int afrejseid)
        {
            this.id = bookingId;
            this.dato = dato;
            this.afrejseId = afrejseid;
        }
        
        public int id { get; set; }
        
        public DateTime dato { get; set; }
        public int afrejseId { get; set; }
        
    }
}
