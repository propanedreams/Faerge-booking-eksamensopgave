using BusinessLogic.BLL;
using Microsoft.AspNetCore.Mvc;
using DTO.Model;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BilController
    {
        [HttpGet]
        [Route("getbilbyid")]
        public Bil getBil(String id)
        {
            BilBLL bll = new BilBLL();
            return bll.getBil(id);
        }

        [HttpGet]
        [Route("getallbil")]

        public List<Bil> getAllBil()
        {
            BilBLL bll = new BilBLL();
            return bll.getAllBil();
        }

        [HttpPost]
        [Route("opretBil")]
        public Bil OpretBil(string nummerplade, int laengde, string model, int bookingId)
        {
            BilBLL bll = new();
            Bil x = new Bil(nummerplade, laengde, model, bookingId);
            return bll.OpretBil(x);
        }

        [HttpGet]
        [Route("getgaesterOnBilById")]
        public List<Gaest> getGaesterOnBilById(string id)
        {
            BilBLL bll = new();
            return bll.GetGaesterOnBil(id);
        }
    }
}

