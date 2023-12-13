
using BusinessLogic.BLL;
using BusinessLogic.FaergeBLL;
using DTO.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaergeController : ControllerBase
    {
        
        [HttpGet]
        [Route("getfaergebyid")]
        public Faerge getFaerge(int id)
        {
            FaergeBLL bll = new FaergeBLL();
            return bll.getFaerge(id);
        }

        [HttpGet]
        [Route("getallfaerges")]
        public List<Faerge> getAllFaerge()
        {
            FaergeBLL bll = new FaergeBLL();
            return bll.getAllFaerge();
        }

       
     
        [HttpPost]
        [Route("opretfaerge")]
        ///
        //tag imod enkle attributter istedet for et helt objekt -> skab en opret faerge i BLL -> oprettelse af objektet her
        ///
        public async Task<ActionResult<Faerge>> OpretFaerge([FromBody] Faerge faergeCreateModel)
        {
            if (faergeCreateModel == null)
            {
                return BadRequest("Invalid data");
            }

            // Validate faergeCreateModel properties as needed
            FaergeBLL bll = new FaergeBLL();
            Faerge createdFaerge = await bll.OpretFaergeAsync(faergeCreateModel);
            if (createdFaerge == null)
            {
                return BadRequest("Unable to create ferry");
            }

            return Ok(createdFaerge);
        }

        //update
        [HttpPut]
        [Route("updatefaerge/{id}")]
        public async Task<ActionResult<Faerge>> UpdateFaerge(int id, [FromBody] Faerge updatedFaerge)
        {
            if (updatedFaerge == null || id != updatedFaerge.id)
            {
                return BadRequest("Invalid data or mismatched id");
            }

            // Validate updatedFaerge properties as needed
            FaergeBLL bll = new FaergeBLL();

            // Check if the ferry with the given id exists
            Faerge existingFaerge = bll.getFaerge(id);
            if (existingFaerge == null)
            {
                return NotFound("Ferry not found");
            }

            // Perform the update
            Faerge updated = await bll.UpdateFaergeAsync(updatedFaerge);

            if (updated == null)
            {
                return BadRequest("Unable to update ferry");
            }

            return Ok(updated);
        }

        [HttpDelete]
        [Route("deletefaerge/{id}")]
        public async Task<ActionResult<Faerge>> deleteFaerge(int id)
        {
            FaergeBLL bll = new FaergeBLL();
            await bll.DeleteFaergeAsync(id);
            return null;
            
        }


        [HttpGet]
        [Route("getallafrejse")]

        public List<Afrejse> getAllAfrejse(int id)
        {
            FaergeBLL bll = new();
            return bll.getFaergeAfrejser(id);
        }

        [HttpGet]
        [Route("getantalgaesterforfaerge")]
        public int CountGaesterFaerge(int id)
        {
            FaergeBLL bll = new FaergeBLL();
            return bll.CountGaesterFaerge(id);
        }
        [HttpGet]
        [Route("getantalbilerforfaerge")]
        public int CountBilerFaerge(int id)
        {
            FaergeBLL bll = new FaergeBLL();
            return bll.CountBilerFaerge(id);
        }

        [HttpGet]
        [Route("getCalculateTotalSumForFaerge")]
        public int CalculateTotalSumForFaerge(int id)
        {
            FaergeBLL bll = new FaergeBLL();
            return bll.CalculateTotalSumForFaerge(id);
        }






    }
}
