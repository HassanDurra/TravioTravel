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
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string isLogedIn   = httpContext.HttpContext.Session.GetString("user") ?? ""; 
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            ViewBag.isLoggedIn = isLogedIn;
            ViewBag.country    = database.Countries.ToList();
            return View("Views/User/Index.cshtml" , isLogedIn);

        }
        // Selecting Country
        public IActionResult getCities(int id)
        {
            var cities = database.Cities.Where(e => e.country_id == id).ToList();
            var Data   = new { city = cities };
            return Json(Data);
        }

    }
}
