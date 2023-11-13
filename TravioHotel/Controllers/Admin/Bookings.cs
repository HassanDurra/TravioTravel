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
                var departureDate = requestData["departureDate"];
                var departureTime = requestData["departureTime"];
                var arrivalDate = requestData["arrivalDate"];
                var arrivalTime = requestData["arrivalTime"];
                // Convert the date strings to DateTime objects
                DateTime parsedDepartureDate, parsedArrivalDate;

                if (DateTime.TryParse($"{departureDate} {departureTime}", out parsedDepartureDate) &&
                    DateTime.TryParse($"{arrivalDate} {arrivalTime}", out parsedArrivalDate))
                {
                    // Format the departure and arrival dates
                    var formattedDepartureDate = parsedDepartureDate.ToString("MMM, dd-yyyy");
                    var formattedDepartureTime = parsedDepartureDate.ToString("h:mm tt");
                    var formattedArrivalDate   = parsedArrivalDate.ToString("MMM, dd-yyyy");
                    var formattedArrivalTime   = parsedArrivalDate.ToString("h:mm tt");


                    // Update the departure and arrival dates
                    bookingData.departure_date = formattedDepartureDate;
                    bookingData.departure_time = formattedDepartureTime;
                    bookingData.arrival_date = formattedArrivalDate;
                    bookingData.arrival_time = formattedArrivalTime;
                    bookingData.created_at   = Convert.ToString(DateTime.UtcNow);
                    // Save changes to the database
                    await Database.SaveChangesAsync();
                    TempData["Success"] = "Ticket Has Been Re-Scheduled";
                    // Redirect to the "Scheduled" action in the "Booking" controller
                    return RedirectToAction("FlightsBookings", "Bookings");
                }
                else
                {
                    TempData["Error"] = "Invalid date(s)";
                }

            }

            TempData["Error"] = "Failed To Re-Schedule The Data";
            return RedirectToAction("Scheduled" , "Bookings");

        }

        // Departed From Origin
        public async Task<IActionResult> Departed(int Id)
        {
            var bookingDetails = await Database.BookingClientDetails.Where(e => e.id == Id).FirstOrDefaultAsync();
            if(bookingDetails != null)
            {
                bookingDetails.status = 1;
                await Database.SaveChangesAsync();
                TempData["Success"] = $"{bookingDetails.firstName} {bookingDetails.lastName} Has Been Departed From Origin";
                return RedirectToAction("FlightsBookings", "Bookings");
            }
            TempData["Error"] = "Failed To Update Status";
            return RedirectToAction("FlightsBookings", "Bookings");
        }

        //Arrived To Destination
        public async Task<IActionResult> Arrived(int Id)
        {
            var bookingDetails = await Database.BookingClientDetails.Where(e => e.id == Id).FirstOrDefaultAsync();
            if (bookingDetails != null)
            {
                bookingDetails.status = 2;
                await Database.SaveChangesAsync();
                TempData["Success"] = $"{bookingDetails.firstName} {bookingDetails.lastName} Has Arrived To Thier Destination";
                return RedirectToAction("FlightsBookings", "Bookings");
            }
            TempData["Error"] = "Failed To Update Status";
            return RedirectToAction("FlightsBookings", "Bookings");
        }
        public async Task<IActionResult> Cancel(int Id , string Status)
        {
            var bookingDetails = await Database.BookingClientDetails.Where(e => e.id == Id).FirstOrDefaultAsync();
            if (bookingDetails != null)
            {
               if(Status == "Scheduled")
                {
                    bookingDetails.status = 3;
                    await Database.SaveChangesAsync();
                    TempData["Success"] = $"Booking For {bookingDetails.firstName} {bookingDetails.lastName} Has been Cancelled";
                    return RedirectToAction("FlightsBookings", "Bookings");
                }
                TempData["Error"] = $"Cannot Cancel Booking For {bookingDetails.firstName} {bookingDetails.lastName} As Flight Has been Departed Or Arrived to its destination ";
                    return RedirectToAction("FlightsBookings", "Bookings");

            }
            TempData["Error"] = "Failed To Update Status";
            return RedirectToAction("FlightsBookings", "Bookings");
        }
        public async Task<IActionResult> Remove(int Id , string Status)
        {
            var bookingDetails = await Database.BookingClientDetails.Where(e => e.id == Id).FirstOrDefaultAsync();
            if (bookingDetails != null)
            {
                if(Status == "valid") {             
                TempData["Success"] = $"Booking Details For {bookingDetails.firstName} {bookingDetails.lastName} Has been Removed";
                Database.BookingClientDetails.Remove(bookingDetails);
                return RedirectToAction("FlightsBookings", "Bookings");
                }
                TempData["Error"] = TempData["Success"] = $"Failed To Remove Booking Details For {bookingDetails.firstName} {bookingDetails.lastName} as Flight Has been Departed From The Origin";
                return RedirectToAction("FlightsBookings", "Bookings");
            }
            TempData["Error"] = $"Failed To Remove Booking Details For {bookingDetails.firstName} {bookingDetails.lastName}";
            return RedirectToAction("FlightsBookings", "Bookings");
        }
    }
}
