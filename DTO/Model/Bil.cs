﻿using DTO.Model.ModelSimpel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Model
{
    public class Bil
    {
        public Bil() { }

        public Bil(string nummerplade, int længdeBil, string model)
        {
            this.nummerplade = nummerplade;
            this.længde = længdeBil;
            this.model = model;
        }

        public Bil(string nummerplade, int længdeBil, string model, int bookingid)
        {
            this.nummerplade = nummerplade;
            this.længde = længdeBil;
            this.model = model;
            this.bookingid = bookingid;
        }


       
        public string nummerplade { get; set; }
       
        public int længde { get; set; }
        public string model { get; set; }
        public int bookingid { get; set; }
        public BookingSimpel booking { get; set; }
    }
}