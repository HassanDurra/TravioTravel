using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using TravioHotel.DataContext;
using Newtonsoft.Json;
namespace TravioHotel.Controllers.User
{  
    public class BookingController : Controller
    {
        public readonly DatabaseContext Database;
        public BookingController (DatabaseContext _database)
        {
            this.Database = _database;
        }
        public IActionResult FlightBooking( string FlightDetails)
        {
            ViewBag.details = FlightDetails;
            return View("Views/User/FlightBooking.cshtml");
        }
        public IActionResult checkCurrency(string Currency)
        {
            var currencySymbol = Database.Countries.Where(e=> e.currency == Currency).FirstOrDefault();
            return Json(currencySymbol);
        }
    }
}
