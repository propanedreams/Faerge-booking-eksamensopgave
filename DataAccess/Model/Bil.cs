using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Bil
    {
        public Bil() { }

        public Bil(string nummerplade, int længdeBil, string model ) {
        this.nummerplade = nummerplade;
            this.længde = længdeBil;
            this.model = model;
        }

        public Bil(string nummerplade, int længdeBil, string model, int bookingid)
        {
            this.nummerplade = nummerplade;
            this.længde = længdeBil;
            this.model = model;
            this.bookingId = bookingid;
        }
        [Key]
        public string nummerplade { get; set; }
        public int længde { get; set; }
        public string model { get; set; }
        public int bookingId { get; set; }
        public Booking booking { get; set; }
    }
}
