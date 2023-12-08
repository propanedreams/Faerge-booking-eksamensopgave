using BusinessLogic.BLL;
using Microsoft.AspNetCore.Mvc;
using DTO.Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GaestController
    {
        [HttpGet]
        [Route("getgaestbyid")]
        public Gaest getAGaest(int id)
        {
            GaestBLL bll = new GaestBLL();
            return bll.getGaest(id);
        }

        [HttpGet]
        [Route("getallgaest")]

        public List<Gaest> getAllGaest()
        {
            GaestBLL bll = new GaestBLL();
            return bll.getAllGaest();
        }

        [HttpPost]
        [Route("opretGaest")]
        public Gaest OpretGaest(string navn, bool koen, string bilId )
        {
            // Validate faergeCreateModel properties as needed
            GaestBLL bll = new();
            Gaest x = new(0, navn, koen, bilId);
            return bll.OpreGaest(x);
        }
    }
}
