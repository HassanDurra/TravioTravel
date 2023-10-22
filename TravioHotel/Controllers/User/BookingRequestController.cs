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
        public async Task<IActionResult> RequestBooking( BookingRequest booking_data)
        {
             string isLogedIn = httpContext.HttpContext.Session.GetString("user") ?? "";
             ViewBag.isLoggedIn = isLogedIn;
            if (ViewBag.isLoggedIn != "")
            {

                var userData    = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(ViewBag.isLoggedIn);
                var saveRequest = new BookingRequest
                {
                    user_id          = userData.id,
                    booking_code     = Guid.NewGuid(),
                    from_country     = booking_data.from_country,
                    from_city        = booking_data.from_city,
                    to_country       = booking_data.to_country,
                    to_city          = booking_data.to_city,
                    departure_date   = booking_data.departure_date,
                    arrival_date     = booking_data.arrival_date,
                    number_of_adults = booking_data.number_of_adults,
                    created_at       = Convert.ToString(DateTime.UtcNow)
                };

                var FromCountryData = await Database.Countries.Where(e => e.id == booking_data.from_country).FirstOrDefaultAsync();
                var ToCountryData   = await Database.Countries.Where(e => e.id == booking_data.to_country).FirstOrDefaultAsync();
                await Database.BookingRequests.AddAsync(saveRequest);
                var SavedData =  Database.SaveChanges();
                if (SavedData > 0)
                {   
                    string EmailBody = $"Dear {userData.name} Your Request For A flight For {FromCountryData.name}/{booking_data.from_city} To {ToCountryData.name}/{booking_data.to_city} For the Date of {booking_data.departure_date} Has been recieved We will notify you within 24/7hrs If there is any flight available for your request <br> Regards : TravioTravel";
                    string Subject   = "Request For Flight Recieved";
                    string userEmail = userData.email;
                    await mailServer.Mail(userEmail, Subject, EmailBody);
                    TempData["Success"] = "Your Request Has Been Sent Check your mail";
                    return RedirectToAction("ThankYou", "Home");
                }

                TempData["Error"] = "Failed To Send Your Request";
                return RedirectToAction("Index", "Home");
            }

            TempData["Error"] = "Before Requesting For A Flight Booking You must Login";
            return RedirectToAction("Login", "Auth");
        }
    }
}
