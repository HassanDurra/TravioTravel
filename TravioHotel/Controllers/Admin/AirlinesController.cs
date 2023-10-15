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
		
		public AirlinesController( DatabaseContext _database )
		{
			this.Database = _database;
		}
		public async Task<IActionResult>Index()
		{
			var airlinesData  = await Database.Airlines.Where(e => e.deleted_at == null).ToListAsync();
            return View("Views/Admin/Airlines/Index.cshtml", airlinesData);
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
		public async Task<IActionResult> addServices(Airlines airlineServices)
		{
			var JsonData = System.IO.File.ReadAllText("D:\\E-project2023\\TravioTravel\\TravioHotel\\wwwroot\\airlines.json");
            JArray jsonArrays = JArray.Parse(JsonData);

            foreach (var jsonArray in jsonArrays)
			{
				var airlineModel = new Airlines() {


				    AirlineImage = (string)jsonArray["AirlineImage"],
				    Airlinename = (string)jsonArray["Airlinename"],
				    ICAOCode = (string)jsonArray["ICAOCode"],
				    IATACode = (string)jsonArray["IATACode"],

                 };
				Database.Airlines.AddAsync(airlineModel);

			}

			var saveChanges  = 	await Database.SaveChangesAsync();

			if(saveChanges > 0)
			{
				TempData["Success"] = "Airlines Services Added to Database Successfully";
				return RedirectToAction("Index", "Airlines");
			}
			TempData["Error"] = "failed To Save Services To Database";
			return RedirectToAction("Index", "Airlines");
			
		}
	}
}
