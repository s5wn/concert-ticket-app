using Microsoft.AspNetCore.Mvc;

namespace ConcertApi.Controllers
{
    public class TicketController : ControllerBase
    {
        [HttpPost]
        public ActionResult RequestConcertTicketPurchase(string id)
        {
            return Problem("Unimplemented");
        }
    }
}