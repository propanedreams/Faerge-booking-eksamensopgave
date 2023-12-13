using DTO.Model.ModelSimpel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Model
{
    public class Afrejse
    {

        public Afrejse
            ()
        { }

        public Afrejse(int afrejseId, DateTime dato)
        {
            this.afrejseId = afrejseId;
            this.dato = dato;
        }

        public Afrejse(int afrejseId, DateTime dato, int faergeid)
        {
            this.afrejseId = afrejseId;
            this.dato = dato;
            this.faergeid = faergeid;
        }



        public int afrejseId { get; set; }

        public DateTime dato { get; set; }

        public int faergeid { get; set; }

        public FaergeSimpel faerge { get; set; }
        public override string ToString()
        {
            return dato.ToString();
        }

    }
}

