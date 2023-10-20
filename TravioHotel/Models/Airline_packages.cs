using System.ComponentModel.DataAnnotations;

namespace TravioHotel.Models
{
    public class Airline_packages
    {
        [Key]
        public int Id { get; set; }
        public int AirlineId { get; set; }
        public string package_type { get; set; }
        public string tax { get; set; }
        public string price { get; set; }

    }
}
