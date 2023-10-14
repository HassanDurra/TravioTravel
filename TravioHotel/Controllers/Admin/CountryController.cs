using Microsoft.AspNetCore.Mvc;

namespace TravioHotel.Controllers.Admin
{
    public class CountryController : Controller
    {
        public readonly IHttpContextAccessor httpContext;
        public CountryController(IHttpContextAccessor _httpContext)
        {
            this.httpContext = _httpContext;
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
            return View("Views/Admin/Countries/Index.cshtml");
        }
    }
}
