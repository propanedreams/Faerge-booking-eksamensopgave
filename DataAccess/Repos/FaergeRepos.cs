using System;
using DTO.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Context;
using DataAccess.Mappers;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.Repos
{
    public class FaergeRepos
    {

        public static Faerge getFaerge(int id)
        {
            using (FaergeContext context = new FaergeContext())
            {
                //Maybe throw exception if not found
                return FaergeMapper.Map(context.Faerge.Find(id));
            }
        }

        public static List<Faerge> getAllFaerge()
        {
            List<Faerge> list = new List<Faerge>();
            using (FaergeContext context = new FaergeContext())
            {
                foreach (DataAccess.Model.Faerge item in context.Faerge)
                {
                    list.Add(FaergeMapper.Map(item));
                }
            }
            return list;
        }

        //post
        public static async Task<Faerge> OpretFaergeAsync(Faerge faerge)
        {
            using (FaergeContext context = new FaergeContext())
            {
                // Validate or perform additional operations if necessary

                context.Faerge.Add(FaergeMapper.Map(faerge));
                await context.SaveChangesAsync();

                return faerge;
            }
        }

        //update/put
        public static async Task<Faerge> UpdateFaergeAsync(Faerge updatedFaerge)
        {
            using (FaergeContext context = new FaergeContext())
            {
                // Retrieve the existing Faerge from the database
                DataAccess.Model.Faerge existingFaerge = await context.Faerge.FindAsync(updatedFaerge.id);

                if (existingFaerge == null)
                {
                    // Handle the case where the Faerge is not found
                    return null;
                }

                // Update the properties of the existing Faerge
                existingFaerge.navn = updatedFaerge.navn;
                existingFaerge.maxAntalBiler = updatedFaerge.maxAntalBiler;
                existingFaerge.maxAntalGaester = updatedFaerge.maxAntalGaester;
                existingFaerge.minAntalBiler = updatedFaerge.minAntalBiler;
                existingFaerge.minAntalGaester = updatedFaerge.minAntalGaester;
                existingFaerge.laengde = updatedFaerge.laengde;
                existingFaerge.prisPrBil = updatedFaerge.prisPrBil;
                existingFaerge.prisPrGaest = updatedFaerge.prisPrGaest;


                // Save changes to the database
                await context.SaveChangesAsync();

                // Return the updated Faerge
                return FaergeMapper.Map(existingFaerge);
            }

        }

        public static async Task<Faerge> DeleteFaergeAsync(int id)
        {
            using (FaergeContext context = new FaergeContext())
            {
                Model.Faerge res = await context.Faerge
                    .FirstOrDefaultAsync(f => f.id == id);
                if (res != null)
                {
                    context.Faerge.Remove(res);
                    await context.SaveChangesAsync();
                    
                }
                return FaergeMapper.Map(res);

            }
             
        }
        public static List<DTO.Model.Afrejse> GetAfgangeForFaerge(int id)
        {
            List<DTO.Model.Afrejse> res = new();
            using (FaergeContext context = new FaergeContext())
            {
                foreach (var x in context.Afrejse.Where(a => a.faergeid == id).Include(a => a.faerge))
                {
                    res.Add(AfrejseMapper.Map(x));
                }
            }
            return res;
        }

        
    

    }
}
