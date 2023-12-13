using System;
using DTO.Model;
using DataAccess.Repos;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient.Server;

namespace BusinessLogic.FaergeBLL
{
    public class FaergeBLL
    {
        public Faerge getFaerge(int id)
        {
            return DataAccess.Repos.FaergeRepos.getFaerge(id);
        }


        public async Task<Faerge> UpdateFaergeAsync(Faerge updatedFaerge)
        {   
            return await FaergeRepos.UpdateFaergeAsync(updatedFaerge);
           
        }

        public async Task<Faerge> DeleteFaergeAsync(int id)
        {
            return await FaergeRepos.DeleteFaergeAsync( id);

        }

        public List<Faerge> getAllFaerge()
        {
            return DataAccess.Repos.FaergeRepos.getAllFaerge();
        }

        //post
        public async Task<Faerge> OpretFaergeAsync(Faerge faergeCreateModel)
        {
            // Validate faergeCreateModel properties as needed

            Faerge faerge = new Faerge
            {
                // Assign properties from faergeCreateModel
                navn = faergeCreateModel.navn,
                minAntalGaester = faergeCreateModel.minAntalGaester,
                maxAntalBiler = faergeCreateModel.maxAntalBiler,
                maxAntalGaester = faergeCreateModel.maxAntalGaester,
                minAntalBiler = faergeCreateModel.maxAntalBiler,
                prisPrBil = faergeCreateModel.prisPrBil,
                prisPrGaest = faergeCreateModel.prisPrGaest,
            };

            // You can add further validation or business logic here before calling the repository
            Faerge createdFaerge = await FaergeRepos.OpretFaergeAsync(faerge);

            return createdFaerge;
        }

        public List<Afrejse> getFaergeAfrejser(int id)
        {
            return DataAccess.Repos.FaergeRepos.GetAfgangeForFaerge(id);
        }

        public int CountGaesterFaerge(int id)
        {
            return DataAccess.Repos.FaergeRepos.CountGaesterFaerge(id);
        }
        public int CountBilerFaerge(int id)
        {
            return DataAccess.Repos.FaergeRepos.CountBilerFaerge(id);
        }
        public int CalculateTotalSumForFaerge(int id)
        {
            return DataAccess.Repos.FaergeRepos.CalculateTotalSumForFaerge(id);
        }


    }
}
