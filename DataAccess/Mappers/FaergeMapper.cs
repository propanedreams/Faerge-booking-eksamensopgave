using DataAccess.Model;
using DTO.Model.ModelSimpel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataAccess.Mappers
{
    internal class FaergeMapper
    {
        public static DTO.Model.Faerge Map(Faerge faerge)
        {
            return new DTO.Model.Faerge(faerge.id, faerge.navn, faerge.maxAntalBiler, faerge.maxAntalGaester, faerge.minAntalBiler, faerge.minAntalGaester, faerge.laengde, faerge.prisPrBil, faerge.prisPrGaest);
        }

        public static Faerge Map(DTO.Model.Faerge faerge)
        {
            return new Faerge(faerge.id, faerge.navn, faerge.maxAntalBiler, faerge.maxAntalGaester, faerge.minAntalBiler, faerge.minAntalGaester, faerge.laengde, faerge.prisPrBil, faerge.prisPrGaest);
        }

        public static List <DTO.Model.Faerge>Map(List<Faerge> faerges)
        {
            List<DTO.Model.Faerge> retur = new List<DTO.Model.Faerge>();
            foreach(Faerge fa in faerges)
            {
                retur.Add(FaergeMapper.Map(fa));
            }
            return retur;
        }

        //-------------------------------------------------------------------------------------------------
        //simple mappers fra dto til simpel og simpel til dataaccess

        public static FaergeSimpel SimpelMap(Faerge f)
        {
            return new FaergeSimpel
            {
                id = f.id,
                navn = f.navn,
                maxAntalBiler = f.maxAntalBiler,
                minAntalBiler = f.minAntalBiler,
                maxAntalGaester = f.maxAntalGaester,
                minAntalGaester = f.minAntalGaester,
                laengde = f.laengde,
                prisPrBil = f.prisPrBil,
                prisPrGaest = f.prisPrGaest
            };
        }
        //simple mappers fra dto til simpel og simpel til dataaccess

        public static Faerge SimpelMap(FaergeSimpel f)
        {
            return new Faerge
            {
                id = f.id,
                navn = f.navn,
                maxAntalBiler = f.maxAntalBiler,
                minAntalBiler = f.minAntalBiler,
                maxAntalGaester = f.maxAntalGaester,
                minAntalGaester = f.minAntalGaester,
                laengde = f.laengde,
                prisPrBil = f.prisPrBil,
                prisPrGaest = f.prisPrGaest
            };
        }
    }
}
