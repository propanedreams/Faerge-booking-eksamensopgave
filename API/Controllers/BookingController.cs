using BusinessLogic.FaergeBLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTO.Model;
using BusinessLogic.BLL;

namespace API.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {

        [HttpGet]
        [Route("getbookingbyid")]
        public Booking getBooking(int id)
        {
            BookingBLL bll = new BookingBLL();
            return bll.getBooking(id);
        }

        [HttpGet]
        [Route("getallbooking")]

        public List<Booking> getAllBooking()
        {
            BookingBLL bll = new BookingBLL();
            return bll.getAllBooking();
        }

        [HttpGet]
        [Route("getbilerbybookingid")]
        public List<Bil> getBilerOnBooking(int id)
        {
            BookingBLL bll = new();
            return bll.GetBilerOnBooking(id);
        }

        [HttpPost]
        [Route("opretBooking")]
        public  Booking OpretBooking(string dato, int afrejseId)
        {
           
            BookingBLL bll = new();
            Booking x = new Booking(0, DateTime.Parse(dato), afrejseId);
             return  bll.OpretBooking(x);
        }

        [HttpDelete]
        [Route("deleteBooking/{id}")]
        public async Task<Booking> deleteBooking(int id)
        {
            BookingBLL bll = new();
            return await bll.DeleteBooking(id);
        }

    }
}
