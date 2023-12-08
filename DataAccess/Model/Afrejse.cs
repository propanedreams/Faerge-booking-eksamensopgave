using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Afrejse
    {
        public Afrejse
            ()
        { }
     //dataaccess reklekterer hvad der gemmes i database
        public Afrejse(int afrejseId, DateTime dato, int faergeid) {
            this.afrejseId = afrejseId;
            this.dato = dato;
            this.faergeid = faergeid;
        }

        [Key]
        public int afrejseId { get; set; }
        public DateTime dato { get; set; }
        public int faergeid { get; set; }
        public Faerge faerge { get; set; }
    }
}
