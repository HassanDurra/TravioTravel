using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
                if (ViewBag.isLoggedIn == "")
                {
                
                    var fromCountry = await Database.Countries.Where(e => e.id == booking_data.from_country).FirstOrDefaultAsync();
                    var toCountry   = await Database.Countries.Where(e => e.id == booking_data.to_country).FirstOrDefaultAsync();

                    var BookingRequestDetails = new
                    {
                        from_country  = fromCountry.iso2,
                        to_country    = toCountry.iso2,
                        from_city     = booking_data.from_city,
                        to_city       = booking_data.to_city,
                        departureDate = booking_data.departure_date,
                        arrivalDate   = booking_data.arrival_date,
                        adults        = booking_data.number_of_adults,
                    };
                    ViewBag.BookingDetails = BookingRequestDetails;
                    return View("Views/User/Listing.cshtml");

                }

                TempData["Error"] = "Before Requesting For A Flight Booking You must Login";
                return View("Views/User/Listing.cshtml");
            
        }
        //This Function Will get The IATA Code from Airports 
        public async Task<IActionResult> getAirportsIata(string fromCity , string toCity, string from_country, string to_country)
        {
                var from_city = await Database.Airports.Where(e => e.City_name == fromCity).FirstOrDefaultAsync();
                var to_city   = await Database.Airports.Where(e => e.City_name == toCity).FirstOrDefaultAsync();
                var fromCountry = await Database.Airports.Where(e => e.Country_iso == from_country).FirstOrDefaultAsync();
                var toCountry = await Database.Airports.Where(e => e.Country_iso == to_country).FirstOrDefaultAsync();

                 if (from_city == null && to_city != null)
                {
                    var Data = new { fromCountryDetails = fromCountry, toCityDetails = to_city, message = "FromCountry" };
                    return Json(Data);
                }
                if(to_city == null && from_city != null)
                {
                    var Data = new { fromCityDetails = from_city, toCountry = toCountry, message = "toCountry" };
                    return Json(Data);
                }
                if (to_city == null && from_city == null)
                {
                    var Data = new { fromCountryDetails = fromCountry, toCountry = toCountry, message = "FromCountryAndCity" };
                    return Json(Data);
                }
                if (fromCity != null && toCity != null)
                {
                    var Data  = new {fromCityDetails = from_city ,  toCityDetails = to_city , message = "FromCity"}; 
                    return Json(Data);
                }
                 var message = new { message = "No Data Found" };
                 return Json(message);   
        }
        //This function will retreive all the airlines that has been retreived by the api of (AMADUES.COM)
        public async Task<IActionResult> getAirlinesService(List<string> AirlinesIata)
        {
            List<Airlines> airlines = new List<Airlines> ();
            if(AirlinesIata != null && AirlinesIata.Any())
            {
                airlines =  Database.Airlines.Where(a => AirlinesIata.Contains(a.IATACode)).ToList();
            }
         
            var data = new { data = airlines };
            return Json(data);
        }
    }
    
}
