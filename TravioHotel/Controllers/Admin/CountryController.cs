using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TravioHotel.DataContext;
using TravioHotel.Models;

namespace TravioHotel.Controllers.Admin
{
    public class CountryController : Controller
    {
        public readonly IHttpContextAccessor httpContext;
        public readonly DatabaseContext Database;
     
        public CountryController(IHttpContextAccessor _httpContext , DatabaseContext  _database)
        {
            this.httpContext = _httpContext;
            this.Database =  _database;
        }
        public IActionResult Index()
        {
            // var loggedIn = httpContext.HttpContext.Session.GetString("admin") ?? "";
            //ViewBag.IsLoggedIn = loggedIn;
            //if (ViewBag.IsLoggedIn == "")
            // {
            //  TempData["Error"] = "You Cannot Access Admin Countries Section Login First";
            //return RedirectToAction("Login", "Auth");
            // }
            var Countries = Database.Countries.ToList(); 
            return View("Views/Admin/Countries/Index.cshtml" , Countries);
        }
    }
}
