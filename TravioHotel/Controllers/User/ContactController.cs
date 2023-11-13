using Microsoft.AspNetCore.Mvc;

namespace TravioHotel.Controllers.User
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View("Views/User/Contact.cshtml");
        }
    }
}
