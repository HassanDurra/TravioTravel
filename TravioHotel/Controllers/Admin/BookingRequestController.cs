using Microsoft.AspNetCore.Mvc;
using TravioHotel.DataContext;
using TravioHotel.Models;

namespace TravioHotel.Controllers.Admin
{
    public class BookingRequestController : Controller
    {
        public readonly DatabaseContext Database;
        public BookingRequestController(DatabaseContext _database)
        {
            this.Database = _database;
        }

        public IActionResult Index()
        {
            var BookingRequestData = Database.BookingRequests.ToList();
            var userIds = BookingRequestData.Select(a => a.user_id).Distinct().ToList();
            var FromCountryIds = BookingRequestData.Select(a => a.from_country).Distinct().ToList();
            var ToCountryIds = BookingRequestData.Select(a => a.to_country).Distinct().ToList();

            var data = (from booking in BookingRequestData
                        join user in Database.User on booking.user_id equals user.Id
                        join fromCountry in Database.Countries on booking.from_country equals fromCountry.id
                        join toCountry in Database.Countries on booking.to_country equals toCountry.id
                        select new
                        {
                            Booking = booking,
                            User = user,
                            FromCountry = fromCountry,
                            ToCountry = toCountry
                        }).ToList();

            ViewBag.Data = data;
            return View("Views/Admin/BookingRequest/Requested.cshtml");

        }
    }
}
