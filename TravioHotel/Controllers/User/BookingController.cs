using Microsoft.AspNetCore.Mvc;
using TravioHotel.DataContext;

namespace TravioHotel.Controllers.User
{  
    public class BookingController : Controller
    {
        public readonly DatabaseContext Database;
        public BookingController (DatabaseContext _database)
        {
            this.Database = _database;
        }
        public IActionResult SearchFlight(string to , string from , string check_in , string check_out , string total_adults)
        {
            var services = Database.Airlines.Where(e => e.deleted_at == null).ToList();
            var Data = new { dropoff = to, destination = from, checkin = check_in, checkout = check_out };
            ViewBag.data = new { service = services, details = Data };
            return View("View/User/Services.cshtml");
        }
    }
}
