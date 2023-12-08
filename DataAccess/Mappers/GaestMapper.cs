using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;
using DTO.Model.ModelSimpel;

namespace DataAccess.Mappers
{
    public class GaestMapper
    {
        public static DTO.Model.Gaest Map(Gaest guest)
        {
            return new DTO.Model.Gaest
            {
                navn = guest.navn,
                gaestId = guest.gaestId,
                køn = guest.køn,
                bilId = guest.bilId,
                bil = BilMapper.MapToSimpel(guest.bil)
            };
        }

            public static Gaest Map(DTO.Model.Gaest guest)
        {
            return new Gaest(guest.gaestId, guest.navn, guest.køn, guest.bilId);
        }

        public static List<DTO.Model.Gaest> Map(List<Gaest> guest)
        {
            List<DTO.Model.Gaest> retur = new List<DTO.Model.Gaest>();
            foreach (Gaest item in guest)
            {
                retur.Add(GaestMapper.Map(item));
            }
            return retur;
        }

        public static GaestSimpel GaestSimpel(Gaest gaest)
        {
            return new GaestSimpel
            {
                gaestId = gaest.gaestId,
                navn = gaest.navn,
                køn = gaest.køn,
                bilId = gaest.bilId,
            };
        }
    }
}
