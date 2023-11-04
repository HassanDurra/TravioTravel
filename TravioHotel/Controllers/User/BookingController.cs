using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using TravioHotel.DataContext;
using Newtonsoft.Json;
namespace TravioHotel.Controllers.User
{  
    public class BookingController : Controller
    {
        public readonly DatabaseContext Database;
        public readonly IWebHostEnvironment fileEnvironment;

        public BookingController (DatabaseContext _database , IWebHostEnvironment fileEnvironment)
        {
            this.Database = _database;
            this.fileEnvironment = fileEnvironment;
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
        // Adding Booking details and Passangers Details 
        public async Task<IActionResult> addBookingDetails(IFormFile[] passanger_image , IFormCollection requestData )
        {
            var airlineDetails = new
            {
                airlineImage = requestData["airline_image"],
                airlineName = requestData["airline_name"],
                aircraftCode = requestData["aircraft_code"],
                journey_type = requestData["journey_type"],
                from = requestData["from"],
                to = requestData["to"],
                departure_date = requestData["departure_date"],
                departure_time = requestData["departure_time"],
                arrival_time = requestData["arrival_time"],
                arrival_date = requestData["arrival_date"],
                flight_duration = requestData["duration"],
                class_type = requestData["class_name"],
                total_price = requestData["currency"] + "" + requestData["total_price"],
                first_name = requestData["first_name[]"],
                last_name = requestData["last_name[]"],
                profileImage = requestData["passanger_image[]"],
            };

            var JsonData = JsonConvert.SerializeObject(airlineDetails);
            ViewBag.Data = JsonData;
            return View("Views/User/Testing.cshtml");


        }
    }
}
