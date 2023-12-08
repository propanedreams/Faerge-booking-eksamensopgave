using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Model.ModelSimpel
{
    public class AfrejseSimpel
    {

        public AfrejseSimpel
            ()
        { }

        public AfrejseSimpel(int afrejseId, DateTime dato)
        {
            this.afrejseId = afrejseId;
            this.dato = dato;
        }

        public AfrejseSimpel(int afrejseId, DateTime dato, int faergeid)
        {
            this.afrejseId = afrejseId;
            this.dato = dato;
            this.faergeid = faergeid;
        }
      
        public int afrejseId { get; set; }
      
        public DateTime dato { get; set; }

        public int faergeid { get; set; }

        

       
    }
}
