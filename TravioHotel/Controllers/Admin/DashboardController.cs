using Microsoft.AspNetCore.Mvc;

namespace TravioHotel.Controllers.Admin
{
    public class DashboardController : Controller
    {   
        public readonly IHttpContextAccessor httpContext;
        public DashboardController( IHttpContextAccessor _httpContext) {
            this.httpContext = _httpContext;
        }    
        public IActionResult Admin()
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var loggedIn       = httpContext.HttpContext.Session.GetString("admin") ?? "";
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            ViewBag.IsLoggedIn = loggedIn;
            if(ViewBag.IsLoggedIn == "" ) {
                TempData["Error"] = "You Cannot Access Admin Dashboard Login First";
                return RedirectToAction("Login", "Auth");
            }
            return View("Views/Admin/Dashboard.cshtml" , loggedIn);
        }
    }
}
