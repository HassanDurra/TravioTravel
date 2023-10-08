using Microsoft.AspNetCore.Mvc;

namespace TravioHotel.Controllers.User
{
    public class AuthController : Controller
    {
        public IActionResult Registeration()
        {
            return View("Views/User/Register.cshtml");
        }
    }
}
