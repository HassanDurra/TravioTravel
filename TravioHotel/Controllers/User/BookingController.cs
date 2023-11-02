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
        public IActionResult FlightBooking( string bookingDetails)
        {
            var details = JsonConvert.DeserializeObject(bookingDetails);
           
            return Json(details);
        }
    }
}
