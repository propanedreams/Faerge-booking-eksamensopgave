using BusinessLogic.BLL;
using BusinessLogic.FaergeBLL;
using Microsoft.AspNetCore.Mvc;
using DTO.Model;
using DTO.Model.ModelSimpel;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AfrejseController
    {
        [HttpGet]
        [Route("getafrejsebyid")]
        public Afrejse getAfrejse(int id)
        {
            AfrejseBLL bll = new AfrejseBLL();
            return bll.getAfrejse(id);
        }

        [HttpGet]
        [Route("getbookingbyafrejseid")]
        public List<Booking> getBookingAfrejse(int id)
        {
            AfrejseBLL bll = new AfrejseBLL();
            return bll.getAfrejseBookings(id);
        }

        [HttpGet]
        [Route("getallafrejse")]

        public List<Afrejse> getAllAfrejse()
        {
            AfrejseBLL bll = new AfrejseBLL();
            return bll.getAllAfrejse();
        }


        [HttpPost]
        [Route("opretAfrejse")]
      
        public async Task<ActionResult<Afrejse>> OpretAfrejse(string dato, int faergeId)
        {
           

            // Validate faergeCreateModel properties as needed
            AfrejseBLL bll = new();
            Afrejse afrejse = new Afrejse(0, DateTime.Parse(dato), faergeId);
            Afrejse createdAfrejse = await bll.OpretAfrejseAsync(afrejse);


            return null ;
        }

        [HttpPut]
        [Route("opdaterAfrejse/{id}")]
        public  Afrejse UpdateFaerge(int id, DateTime dato)
        {
            // Validate updatedFaerge properties as needed
            AfrejseBLL bll = new();
            Afrejse af = new(id, dato);
            // Perform the update
            Afrejse updated = bll.OpdaterAfrejse(af);
            return null;
        }


        [HttpDelete]
        [Route("deleteAfrejse/{id}")]
        public async Task<Afrejse> deleteFaerge(int id)
        {
            AfrejseBLL bll = new();
           await bll.DeleteAfrejse(id);
            return null;

        }

    }
}

