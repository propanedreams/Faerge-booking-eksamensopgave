using DTO.Model.ModelSimpel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Model
{
    public class Gaest
    {
        public Gaest() { }
        public Gaest(string navn, bool køn)
        {

            this.navn = navn;
            this.køn = køn;

        }
        public Gaest(int gaestId, string navn, bool køn)
        {
            this.gaestId = gaestId;
            this.navn = navn;
            this.køn = køn;

        }

        public Gaest(int gaestId, string navn, bool køn, string bilid)
        {
            this.gaestId = gaestId;
            this.navn = navn;
            this.køn = køn;
            this.bilId = bilid;
        }
       
        public int gaestId { get; set; }
        
        public string navn { get; set; }

        public bool køn { get; set; }

        public string bilId { get; set; }
        public BilSimpel bil { get; set; }

    }
}
