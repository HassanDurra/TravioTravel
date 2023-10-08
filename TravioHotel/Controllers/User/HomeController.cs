using Microsoft.AspNetCore.Mvc;

namespace TravioHotel.Controllers.User
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
         
            return View("Views/User/Index.cshtml");
        }
    }
}
