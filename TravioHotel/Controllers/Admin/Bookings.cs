using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Globalization;
using TravioHotel.DataContext;
using TravioHotel.Models;

namespace TravioHotel.Controllers.Admin
{
    public class Bookings : Controller
    {
        public readonly DatabaseContext Database;
        public Bookings(DatabaseContext _database)
        {
            this.Database = _database;
        }

        public IActionResult FlightsBookings()
        {
            var passangersDetails = Database.BookingClientDetails.ToList();
            var flightIds = passangersDetails.Select(passanger => passanger.flight_details_id).Distinct();
            var FlightDetails = Database.BookingFlightDetails.Where(e => flightIds.Contains(e.id)).ToList();
            var JoinedData = from passanger in passangersDetails
                             join flight in FlightDetails on passanger.flight_details_id equals flight.id
                             select new { passangers = passanger, flights = flight };
          
            ViewBag.Data = JoinedData;
            return View("Views/Admin/Bookings/Flights.cshtml");

        }

        public IActionResult Scheduled(int id)
        {
            var Id = (int)id;
            ViewBag.Id = new { id = Id };
            return View("Views/Admin/Bookings/rescheduleFlight.cshtml");
        }
        public async Task<IActionResult> updateTime(IFormCollection requestData , BookingFlightDetails bookingDetails , int flightId)
        {
             var bookingData = await Database.BookingFlightDetails
            .Where(e => e.id == Convert.ToInt32(requestData["Id"]))
            .FirstOrDefaultAsync();

            if (bookingData != null)
            {
            
               
            }

            TempData["Error"] = "Failed To Re-Schedule The Data";
            return RedirectToAction("Scheduled" , "Booking");

        }
    }
}
