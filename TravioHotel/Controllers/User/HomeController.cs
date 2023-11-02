using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TravioHotel.DataContext;

namespace TravioHotel.Controllers.User
{

    public class HomeController : Controller
    {
       public readonly IHttpContextAccessor httpContext;
        public readonly DatabaseContext database;
        public HomeController(IHttpContextAccessor _httpContext , DatabaseContext _database)
        {
            this.httpContext        =   _httpContext;
            this.database           =   _database;
        }
        public IActionResult Index()
        {

            string isLogedIn   = httpContext.HttpContext.Session.GetString("user") ?? ""; 
            
            ViewBag.isLoggedIn = isLogedIn;
            ViewBag.country = database.Countries.ToList();
            return View("Views/User/Index.cshtml");

        }
        // Selecting Country
        public IActionResult getCities(int id)
        {
            var cities = database.Cities.Where(e => e.country_id == id).ToList();
            var Data   = new { city = cities };
            return Json(Data);
        }
        public IActionResult Listing()
        {
            return View("Views/User/Listing.cshtml");
        }
    }
}
