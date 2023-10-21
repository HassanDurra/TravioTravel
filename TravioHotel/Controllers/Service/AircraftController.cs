using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Cryptography.Pkcs;
using TravioHotel.DataContext;
using TravioHotel.Models;
using TravioHotel.Services;

namespace TravioHotel.Controllers.Service
{
    public class AircraftController : Controller
    {
        public readonly DatabaseContext Database;
        public readonly MailServer mailServer;
        public readonly IWebHostEnvironment fileEnvironment;
        public readonly IHttpContextAccessor httpContext;


        public AircraftController(  IHttpContextAccessor _httpContext, DatabaseContext _database, IWebHostEnvironment _fileEnvironment, MailServer _mailServer)
        {
            this.Database = _database;
            this.mailServer = _mailServer;
            this.fileEnvironment = _fileEnvironment;
            this.httpContext = _httpContext;

        }
        public IActionResult Index()
        {
            var AircraftInformation = Database.Aircrafts.ToList();

            return View();
        }

        public IActionResult Create()
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var userData = httpContext.HttpContext.Session.GetString("service");
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            if( userData != "" && userData != null)
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                dynamic userIdData = JsonConvert.DeserializeObject(userData);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

                // Assuming "id" is the property containing the user ID
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                int userId = userIdData.id;
#pragma warning restore CS8602 // Dereference of a possibly null reference.

                var Service_account_id = Database.Service_Account.Where(e => e.userId == userId).FirstOrDefault();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                TempData["agent_id"]   = Service_account_id.Id;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                TempData["action"]     = "create";
                return View("Views/Service/Aircraft/Create.cshtml");
            }
            TempData["Error"] = "Please Login Before Accessing Agent Dashboard";
            return RedirectToAction("Login", "Auth");
        }
        [HttpPost]
        public async Task<IActionResult>Store(Aircraft aircraftData , IFormFile aircraft_image)
        {
            if(aircraft_image != null)
            {
                var filename = Guid.NewGuid() + Path.GetExtension(aircraft_image.FileName);
                var filepath = Path.Combine(fileEnvironment.WebRootPath, "AircraftImages");
                // Check if directory not exists so it will create a new directory
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                // Combining the file path with image name then saving them
                var AirCraftImage = Path.Combine(filepath, filename);
                using (var saveImage = new FileStream(AirCraftImage, FileMode.Create))
                {
                    aircraft_image.CopyTo(saveImage);
                } ;
                var AircraftData = new Aircraft
                {
                    agent_id              = aircraftData.agent_id ,
                    aircraft_image        = filename ,
                    aircraft_model_name   = aircraftData.aircraft_model_name ,
                    aircraft_model_number = aircraftData.aircraft_model_number ,
                    total_seats           = aircraftData.total_seats ,
                    remaining_seats       = aircraftData.remaining_seats, 
                    bussiness_seats       = aircraftData.bussiness_seats ,
                    first_class_seats     = aircraftData.first_class_seats,

                };
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                Database.Aircrafts.AddAsync(AircraftData);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                var savedAircrafts = await Database.SaveChangesAsync();
                if(savedAircrafts > 0)
                {
                    TempData["Success"] = "Aircraft Information Has been Saved";
                    return RedirectToAction("Create", "Aircraft");
                }
                TempData["Error"] = "Error Saving Aircraft Information";
                return RedirectToAction("Create", "Aircraft");
            }
            TempData["Error"] = "An Error occured While Saving Aircraft Information";
            return RedirectToAction("Create", "Aircraft");
        }
    }
}
