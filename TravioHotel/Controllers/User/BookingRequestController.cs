using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravioHotel.DataContext;
using TravioHotel.Models;
using TravioHotel.Services;

namespace TravioHotel.Controllers.User
{
    public class BookingRequestController : Controller
    {
        public readonly DatabaseContext Database;
        public readonly IHttpContextAccessor httpContext;
        public readonly MailServer mailServer;

        public BookingRequestController(DatabaseContext _database , IHttpContextAccessor _httpContext , MailServer _mailServer) {
                this.Database    = _database; 
                this.httpContext = _httpContext;    
                this.mailServer  = _mailServer;
        }
        public async Task<IActionResult> RequestBooking(BookingRequest booking_data)
        {
            string isLogedIn = httpContext.HttpContext.Session.GetString("user") ?? "";
            ViewBag.isLoggedIn = isLogedIn;
            if (ViewBag.isLoggedIn != "")
            {
                
                    var fromCountry = await Database.Cities.Where(e => e.name == booking_data.from_city).FirstOrDefaultAsync();
                    var toCountry = await Database.Cities.Where(e => e.name == booking_data.to_city).FirstOrDefaultAsync();

                    var BookingRequestDetails = new
                    {
                        from_country  = fromCountry.country_code,
                        to_country    = toCountry.country_code,
                        from_city     = booking_data.from_city,
                        to_city       = booking_data.to_city,
                        departureDate = booking_data.departure_date,
                        arrivalDate   = booking_data.arrival_date,
                    };
                    ViewBag.BookingDetails = BookingRequestDetails;
                    return View("Views/User/Listing.cshtml");

                }

                TempData["Error"] = "Before Requesting For A Flight Booking You must Login";
                return View("Views/User/Listing.cshtml");
            
        }
        //This Function Will get The IATA Code from Airports 
        public async Task<IActionResult> getAirportsIata(string fromCity , string toCity, string fromCountryIso  , string toCountryIso)
        {
                var from_city = await Database.Airports.Where(e => e.City_name == fromCity).FirstOrDefaultAsync();
                var to_city   = await Database.Airports.Where(e => e.City_name == toCity).FirstOrDefaultAsync();
                if(from_city == null || to_city == null)
                {
                var fromCountry  = await Database.Airports.Where(e => e.Country_iso == fromCountryIso).FirstOrDefaultAsync();
                var toCountry    = await Database.Airports.Where(e => e.Country_iso == toCountryIso).FirstOrDefaultAsync();
                var Data = new { fromCountryDetails = fromCountry, toCountryDetails = toCountry, message = "FromCountry" };
                return Json(Data);
                }
                if(fromCity != null && toCity != null)
                {
                    var Data  = new {fromCityDetails = from_city ,  toCityDetails = to_city , message = "FromCity"}; 
                    return Json(Data);
                }
                 var message = new { message = "No Data Found" };
                 return Json(message);   
        }
    }
}
