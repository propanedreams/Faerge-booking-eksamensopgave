using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Model.ModelSimpel
{
    public class GaestSimpel
    {
        public GaestSimpel() { }
        public GaestSimpel(string navn, bool køn)
        {

            this.navn = navn;
            this.køn = køn;

        }
        public GaestSimpel(int gaestId, string navn, bool køn)
        {
            this.gaestId = gaestId;
            this.navn = navn;
            this.køn = køn;

        }

        public GaestSimpel(int gaestId, string navn, bool køn, string bilid)
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
     

    }
}
