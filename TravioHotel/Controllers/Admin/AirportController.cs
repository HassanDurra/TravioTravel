using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravioHotel.DataContext;
using TravioHotel.Models;

namespace TravioHotel.Controllers.Admin
{

    public class AirportController : Controller
    {
        public readonly DatabaseContext Database;
        public AirportController(DatabaseContext _database)
        {
            this.Database = _database;
        }

        public async Task<IActionResult> Index()
        {
            var airports = await Database.Airports.Where(e => e.delete_at == null).ToListAsync();
            var uniqueCountryIsos = airports.Where(a => a.delete_at == null).Select(a => a.Country_iso).Distinct();

            Countries countries = null;
            if (uniqueCountryIsos.Any())
            {
                countries = await Database.Countries.Where(c => uniqueCountryIsos.Contains(c.iso2)).FirstAsync();
            }

            ViewBag.data = new { Airports = airports, Country = countries };
            return View("Views/Admin/Airports/Index.cshtml");
        }
        // Trashed
        public async Task<IActionResult> Trash()
        {
            var airports = await Database.Airports.Where(e => e.delete_at != null).ToListAsync();

            var uniqueCountryIsos = airports.Where(a => a.delete_at != null).Select(a => a.Country_iso).Distinct();

            Countries countries = null;
            if (uniqueCountryIsos.Any())
            {
                countries = await Database.Countries.Where(c => uniqueCountryIsos.Contains(c.iso2)).FirstAsync();
            }

            ViewBag.data = new { Airports = airports, Country = countries };
            return View("Views/Admin/Airports/Trash.cshtml");
        }
        // Create View
        public async Task<IActionResult> Create()
        {
            TempData["action"] = "Create";
            var countries = await Database.Countries.ToListAsync();
            var airports = await Database.Airports.ToListAsync();
            ViewBag.data = new { country = countries, airport = airports };
            return View("Views/Admin/Airports/Create.cshtml");
        }
        // Edit
        public async Task<IActionResult> Edit(int? id)
        {
            TempData["action"] = "Edit";
            var countries = await Database.Countries.ToListAsync();
            var airports = await Database.Airports.FirstOrDefaultAsync(e => e.Id == id);
            ViewBag.data = new { country = countries, airport = airports };
            return View("Views/Admin/Airports/Create.cshtml", airports);
        }
        public async Task<IActionResult> Store(Airport airport)
        {
            var airports = await Database.Airports.FirstOrDefaultAsync(e => e.Name == airport.Name);
            if (airports == null)
            {
                var saveAirport = new Airport()
                {
                    Name = airport.Name,
                    Image = airport.Image,
                    Country_iso = airport.Country_iso,
                    Description = airport.Description,
                    City_name   = airport.City_name,
                    IataCode = airport.IataCode,
                    IcaoCode = airport.IcaoCode
                };
                Database.Airports.AddAsync(saveAirport);
                var SavedData = await Database.SaveChangesAsync();
                if (SavedData > 0)
                {
                    TempData["Success"] = "Airport Information Has been Saved";
                    return RedirectToAction("Index", "Airport");
                }
                TempData["Error"] = "Failed To Save Airport Information";
                return RedirectToAction("Create", "Airport");
            }
            TempData["Error"] = "Airport Name Already Exists";
            return RedirectToAction("Create", "Airport");

        } // Storing Data For Airport Information Ends here

        // Updating Information
        public async Task<IActionResult> Update(Airport oldAirports)
        {
            var Airports = await Database.Airports.FirstOrDefaultAsync(e => e.Id == oldAirports.Id);
            if (Airports != null)
            {
                Airports.Name = oldAirports.Name;
                Airports.Image = oldAirports.Image;
                Airports.Description = oldAirports.Description;
                Airports.Country_iso = oldAirports.Country_iso;
                Airports.City_name = oldAirports.City_name;
                Airports.IataCode = oldAirports.IataCode;
                Airports.IcaoCode = oldAirports.IcaoCode;
                await Database.SaveChangesAsync();
                TempData["Success"] = "Airport Information Has Been Saved";
                return RedirectToAction("Index", "Airport");
            }
            TempData["Error"] = "Failed to Update Airport Information";
            return RedirectToAction("Index", "Airport");
        } // THIS WILL UPDATE THE INFORMATION OF AIRPORT
        // Sending Data to Trash
        public async Task<IActionResult> Delete(int? id)
        {
            var Airport = await Database.Airports.FirstOrDefaultAsync(e => e.Id == id);
            var currentDate = Convert.ToString(DateTime.UtcNow);
            if (Airport != null)
            {
                Airport.delete_at = currentDate;
                await Database.SaveChangesAsync();

                TempData["Success"] = "Airport Information has been stored in Trash";
                return RedirectToAction("Index", "Airport");
            }
            TempData["Error"] = "Failed To Store Airport Information into Trash";
            return RedirectToAction("Index", "Airport");

        }
        // This Will Restore the data from trash
        public async Task<IActionResult> Restore(int? id)
        {
            var Airport = await Database.Airports.FirstOrDefaultAsync(e => e.Id == id);
            if (Airport != null)
            {
                Airport.delete_at = null;
                await Database.SaveChangesAsync();

                TempData["Success"] = "Airport Information has been restored ";
                return RedirectToAction("Index", "Airport");
            }
            TempData["Error"] = "Failed To Restore Airport Information";
            return RedirectToAction("Index", "Airport");

        }
        // Deleting Parmanently
        public async Task<IActionResult> Destroy(int? id)
        {
            var Airport = await Database.Airports.FirstOrDefaultAsync(e => e.Id == id);
            if (Airport != null)
            {
                Database.Airports.Remove(Airport);
                await Database.SaveChangesAsync();
                TempData["Success"] = "Airport Information has been Deleted ";
                return RedirectToAction("Index", "Airport");
            }
            TempData["Error"] = "Failed To Delete Airport Information";
            return RedirectToAction("Index", "Airport");

        }
        // Getting All Cities Based on Country Id 
        public IActionResult GetCities(Airport airport)
        {
           
                var countries = Database.Cities.Where(c => c.country_code == airport.Country_iso).ToList();
                var Data = new { message = "Success", city = countries };
                return Json(Data);
        }
    }
}
