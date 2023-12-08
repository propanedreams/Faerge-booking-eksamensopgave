using DataAccess.Model;
using DTO.Model.ModelSimpel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Mappers
{
    internal class AfrejseMapper
    {
        public static DTO.Model.Afrejse Map(Afrejse afrejse)
        {
            DTO.Model.Afrejse x = new DTO.Model.Afrejse(afrejse.afrejseId, afrejse.dato, afrejse.faergeid);
            x.faerge = FaergeMapper.SimpelMap(afrejse.faerge);
            return x;
        }

        public static DTO.Model.Afrejse MapS(Afrejse afrejse)
        {
            DTO.Model.Afrejse x = new DTO.Model.Afrejse(afrejse.afrejseId, afrejse.dato, afrejse.faergeid);
            return x;
        }

        //det fuckeede med systemet at jeg hentede en dto.model og sætte en færge på den påvej ned i databasen
        public static Afrejse Map(DTO.Model.Afrejse afrejse)
        {
           Afrejse x = new Afrejse(afrejse.afrejseId, afrejse.dato, afrejse.faergeid);
          //  x.faerge = FaergeMapper.SimpelMap(afrejse.faerge);
            return x;
        }

        public static List<DTO.Model.Afrejse> Map(List<Afrejse> afrejse)
        {
            List<DTO.Model.Afrejse> retur = new List<DTO.Model.Afrejse>();
            foreach (Afrejse item in afrejse)
            {
                DTO.Model.Afrejse x = new DTO.Model.Afrejse
                {
                    afrejseId = item.afrejseId,
                    dato =item.dato,
                    faergeid = item.faergeid,
                };
                x.faerge = FaergeMapper.SimpelMap(item.faerge);
                retur.Add(x);
            }
            return retur;
        }
     

        //simple mappers
       
        //Dataaccess -> simpelmodel -> DTO.model
        //skal fixes her

        public static AfrejseSimpel MapToSimpelX(Afrejse afrejse)
        {
            return new AfrejseSimpel
            {
                afrejseId = afrejse.afrejseId,
                dato = afrejse.dato,
                faergeid = afrejse.faergeid
                // map other properties as needed
            };
        }


        public static DTO.Model.ModelSimpel.AfrejseSimpel SimpelMap(Afrejse afrejse)
        {
            return  new DTO.Model.ModelSimpel.AfrejseSimpel(afrejse.afrejseId, afrejse.dato, afrejse.faergeid);
            //a.faergeSimpel = FaergeMapper.Map(afrejse.faerge);
            //return a;
        }


    }
}
