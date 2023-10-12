using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace TravioHotel.Controllers.User
{

    public class HomeController : Controller
    {
       public readonly IHttpContextAccessor httpContext;
        public HomeController(IHttpContextAccessor _httpContext)
        {
            this.httpContext        =   _httpContext;
        }
        public IActionResult Index()
        {
            string isLogedIn = httpContext.HttpContext.Session.GetString("user") ?? ""; 
            ViewBag.isLoggedIn = isLogedIn;
         
            return View("Views/User/Index.cshtml" , isLogedIn);

        }
    }
}
