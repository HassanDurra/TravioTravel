using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using TravioHotel.DataContext;
using Newtonsoft.Json;
using TravioHotel.Models;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Http;


namespace TravioHotel.Controllers.User
{
    public class BookingController : Controller
    {
        public readonly DatabaseContext Database;
        public readonly IWebHostEnvironment fileEnvironment;
        public readonly IHttpContextAccessor httpContext;
  
        public BookingController(DatabaseContext _database, IWebHostEnvironment fileEnvironment , IHttpContextAccessor _httpContext)
        {
            this.Database = _database;
            this.fileEnvironment = fileEnvironment;
            this.httpContext = _httpContext;
       
        }
        public IActionResult FlightBooking(string FlightDetails)
        {
            string isLogedIn = httpContext.HttpContext.Session.GetString("user") ?? "";
            ViewBag.isLoggedIn = isLogedIn;
            if (isLogedIn != "")
            {
               
             
                ViewBag.details = FlightDetails;
                return View("Views/User/FlightBooking.cshtml");
            }
            TempData["Error"] = "Before Booking Tickets You Must Login To Your Account";
            return RedirectToAction("Login" , "Auth");
        }
        public IActionResult checkCurrency(string Currency)
        {
            var currencySymbol = Database.Countries.Where(e => e.currency == Currency).FirstOrDefault();
            return Json(currencySymbol);
        }
        // Adding Booking details and Passangers Details 
        public async Task<IActionResult> addBookingDetails(IFormFileCollection passanger_images, IFormCollection requestData)
        {
            var createdDate    = Convert.ToString(DateTime.UtcNow);
            var userId        = Convert.ToInt64(requestData["user_id"]);
   
            var airlineDetails = new BookingFlightDetails
            {
                airline_image = requestData["airline_image"],
                airline_name = requestData["airline_name"],
                air_craft_id = requestData["aircraft_code"],
                journey_type = requestData["journey_type"],
                from = requestData["from"],
                to = requestData["to"],
                departure_date = requestData["departure_date"],
                departure_time = requestData["departure_time"],
                arrival_time = requestData["arrival_time"],
                arrival_date = requestData["arrival_date"],
                flight_duration = requestData["duration"],
                class_type = requestData["class_name"],
                total_price = requestData["currency"] + "" + requestData["total_price"],
                created_at = createdDate ,
                user_id    = (int)userId
            };
            await Database.BookingFlightDetails.AddAsync(airlineDetails);
            var addAirlinesDetails = await Database.SaveChangesAsync();
            if (addAirlinesDetails > 0) {
                List<BookingFlightClientDetails> passangersDetailsList = new List<BookingFlightClientDetails>();

                for (int index = 0; index < requestData["email[]"].Count; index++)
                {
                    Random random = new Random();
                    long maxNumber = 9999999999;
                    long randomNumericNumber = (long)(random.NextDouble() * (maxNumber - 1000000000) + 1000000000);
                    string randomNumericString = "BKN" + "-" + randomNumericNumber.ToString();
                    var FileName = requestData["email[]"][index] + Path.GetExtension(passanger_images[index].FileName);
                    var FilePath = Path.Combine(fileEnvironment.WebRootPath, "PassangersImages");
                    if (!Directory.Exists(FilePath))
                    {
                        Directory.CreateDirectory(FilePath);
                    }
                    var PassangerImageProfile = Path.Combine(FilePath, FileName);
                    using (var SaveImage = new FileStream(PassangerImageProfile, FileMode.Create))
                    {
                        passanger_images[index].CopyTo(SaveImage);
                    }
                    // Above Code will get the path and it will store the image to the local Folder;  
                    var passangersDetailsAdd = new BookingFlightClientDetails
                    {
                        flight_details_id = airlineDetails.id,
                        firstName = requestData["first_name[]"][index],
                        lastName = requestData["last_name[]"][index],
                        passport_number = requestData["passport_number[]"][index],
                        Cnic_number = requestData["cnic_number[]"][index],
                        country_name = requestData["country_name[]"][index],
                        city_name = requestData["city[]"][index],
                        email = requestData["email[]"][index],
                        contact_number = requestData["phone_number[]"][index],
                        date_of_birth = requestData["date_of_birth[]"][index],
                        age = requestData["age[]"][index],
                        image = FileName,
                        created_at = createdDate,
                        Booking_Number = randomNumericString,
                        payment_method = "EasyPaisa"

                    };
                    passangersDetailsList.Add(passangersDetailsAdd);
                    await Database.BookingClientDetails.AddAsync(passangersDetailsAdd);
                
                }
                var passangersDetailsSave = await Database.SaveChangesAsync();

                if(passangersDetailsSave > 0)
                {
                    TempData["Success"] = "Booking Has Been successful";
                    TempData["Passangers"] = JsonConvert.SerializeObject(passangersDetailsList);
                    return RedirectToAction("ThankYou" , "Booking");
                }

            }
            ViewBag.Data = "Error";
            return View("Views/User/Testing.cshtml");

        }

        public IActionResult ThankYou()
        {
            ViewBag.Data = TempData["Passangers"] ;
            return View("Views/User/Thankyou.cshtml");
        }
        // Pdf Generate
        
    }
}
