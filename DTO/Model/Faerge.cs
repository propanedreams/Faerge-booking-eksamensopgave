using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Model
{
    public class Faerge
    {
        public Faerge(int id, string navn, int maxAntalBiler, int maxAntalGaester, int minAntalBiler, int minAntalGaester, int laengde, int prisPrBil, int prisPrGaest)
        {
            this.id = id;
            this.navn = navn;
            this.maxAntalBiler = maxAntalBiler;
            this.maxAntalGaester = maxAntalGaester;
            this.minAntalBiler = minAntalBiler;
            this.minAntalGaester = minAntalGaester;
            this.laengde = laengde;
            this.prisPrBil = prisPrBil;
            this.prisPrGaest = prisPrGaest;

        }

        public Faerge(string navn, int maxAntalBiler, int maxAntalGaester, int minAntalBiler, int minAntalGaester, int laengde)
        {

            this.navn = navn;
            this.maxAntalBiler = maxAntalBiler;
            this.maxAntalGaester = maxAntalGaester;
            this.minAntalBiler = minAntalBiler;
            this.minAntalGaester = minAntalGaester;
            this.laengde = laengde;
           

        }

        public Faerge()
        {

        }
        
        public int id { get; set; }
        
        public string navn { get; set; }
        
        public int maxAntalBiler
        {
            get;
            set;
        }
        public int maxAntalGaester { get; set; }
        public int minAntalBiler { get; set; }
        public int minAntalGaester { get; set; }
        public int laengde { get; set; }
        
        public int prisPrBil { get; set;}
        public int prisPrGaest { get; set;}


        public override string ToString()
        {
            return navn;
        }
    }
}

