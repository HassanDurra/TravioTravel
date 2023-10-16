using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TravioHotel.CustomClasses;
using TravioHotel.DataContext;
using TravioHotel.Models;

namespace TravioHotel.Controllers.Admin
{
	public class AirlinesController : Controller
	{
		public readonly DatabaseContext Database;
		public readonly IHttpContextAccessor httpContext;
		public AirlinesController( DatabaseContext _database , IHttpContextAccessor _httpContext )
		{
			this.Database = _database;
			this.httpContext = _httpContext;
		}
		public async Task<IActionResult>Index()
		{
            
		  var userData  = httpContext.HttpContext.Session.GetString("admin"); // Then we will store it to session
		  ViewBag.isLoggedIn = userData;
			if (userData != "" || userData != null)
			{
                var airlinesData = await Database.Airlines.Where(e => e.deleted_at == null).ToListAsync();
                return View("Views/Admin/Airlines/Index.cshtml", airlinesData);
            }
			TempData["Error"] = "Please Login First To Access Admin Dashboard";
			return RedirectToAction("Login", "Auth");

        }public async Task<IActionResult>Trash()
		{
			var airlinesData  = await Database.Airlines.Where(e => e.deleted_at != null).ToListAsync();
            return View("Views/Admin/Airlines/Trash.cshtml", airlinesData);
        }

		public IActionResult create()
		{
			TempData["action"] = "create";
			return View("Views/Admin/Airlines/Create.cshtml");
		}
		public async Task<IActionResult>Edit(int ? id)
		{
			var airlinesData = await Database.Airlines.FirstOrDefaultAsync(e => e.Id == id);
			TempData["action"] = "edit";
			return View("Views/Admin/Airlines/Create.cshtml" , airlinesData);
		}
		// This will Insert Data from form Submission
		public async Task<IActionResult>Store(Airlines airline)
		{
			var checkAirlines  = await Database.Airlines.FirstOrDefaultAsync(e => e.Airlinename == airline.Airlinename);
			if (checkAirlines == null) { 
			var insertData = new Airlines()
			{
				AirlineImage = airline.AirlineImage ,
				Airlinename  = airline.Airlinename ,
                ICAOCode     = airline.ICAOCode ,
				IATACode     = airline.IATACode ,
			};

			Database.Airlines.AddAsync(insertData);
			var savedData = await Database.SaveChangesAsync();

			if(savedData > 0)
			{
				TempData["Success"] = "Airline Has Been Saved";
				return RedirectToAction("Index", "Airlines");
			}
			TempData["Error"] = "Failed to Add Airline Service";
			return RedirectToAction("Create", "Airlines");
            }
			TempData["Error"] = "This Airline Is Already Exists ";
			return RedirectToAction("Create", "Airlines");
        }
		// Updating All Records
		public async Task<IActionResult>Update( Airlines airlines)
		{
			var AirlineData = await Database.Airlines.FirstOrDefaultAsync(e=>e.Id == airlines.Id);
			if(AirlineData != null)
			{

               AirlineData.AirlineImage = airlines.AirlineImage;
               AirlineData.Airlinename  = airlines.Airlinename ;
			   AirlineData.ICAOCode     = airlines.ICAOCode ;
			   AirlineData.IATACode     = airlines.IATACode ;			
				await Database.SaveChangesAsync();
				TempData["Success"] = "Airline Information Has been Updated";
				return RedirectToAction("Index", "Airlines");
			}
			TempData["Error"] = "Failed to update Airline Information";
			return RedirectToAction("Index", "Airlines");
		}
		// Removing The Airlines
		public async Task<IActionResult>Delete(int? id)
		{
			var airlineData = await Database.Airlines.FirstOrDefaultAsync(e => e.Id == id);
            var currentData = Convert.ToString(DateTime.UtcNow);

            if (airlineData != null)
			{
				var UpdateData =  airlineData.deleted_at = currentData;
				
				await Database.SaveChangesAsync();
				TempData["Success"] = "Airline Has been Added to Trash";
				return RedirectToAction("Index", "Airlines");
			}
            TempData["Error"] = "Failed to Add Airline To Trash";
            return RedirectToAction("Index", "Airlines");
        }
		//Restoring Data From Trash
		public async Task<IActionResult>Restore(int? id)
		{
			var airlineData = await Database.Airlines.FirstOrDefaultAsync(e => e.Id == id);
    
			if (airlineData != null)
			{
				var UpdateData = airlineData.deleted_at = null;

				await Database.SaveChangesAsync();
				TempData["Success"] = "Airline Has been Restored";
				return RedirectToAction("Index", "Airlines");

            }
            TempData["Error"] = "Failed to Restore Airline From Trash";
            return RedirectToAction("Index", "Airlines");
        }
		// Deleting Data Permantly
		public async Task<IActionResult> Destroy(int? id)
		{
            var airlineData = await Database.Airlines.FirstOrDefaultAsync(e => e.Id == id);

            if (airlineData != null)
            {
                Database.Airlines.Remove(airlineData);
                await Database.SaveChangesAsync();
                TempData["Success"] = "Airline Has been Deleted";
                return RedirectToAction("Index", "Airlines");

            }
            TempData["Error"] = "Failed to Delete Airline ";
            return RedirectToAction("Index", "Airlines");
        }
		// This Will Insert Data from JsonFile
	}
}
