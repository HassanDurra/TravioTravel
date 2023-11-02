using Microsoft.AspNetCore.Mvc;
using TravioHotel.DataContext;

namespace TravioHotel.Controllers.Admin
{
    public class BookingRequestController : Controller
    {
        public readonly DatabaseContext Database;
        public BookingRequestController(DatabaseContext _database)
        {
            this.Database = _database;
        }


    }
}
