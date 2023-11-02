using System.ComponentModel.DataAnnotations;

namespace TravioHotel.Models
{
    public class BookingRequest
    {
        [Key]
        public int Id { get; set; }

        public string from_city { get; set; }
        public string to_city { get; set; }
        public int from_country { get; set; }
        public int to_country { get; set; }
        public string departure_date { get; set; }  
        public string? created_at { get; set; }
        public string? deleted_at { get; set; }
        
        public int number_of_adults { get; set; }
    }
}
