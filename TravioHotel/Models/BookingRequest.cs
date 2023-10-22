using Org.BouncyCastle.Asn1.Cms;
using System.ComponentModel.DataAnnotations;

namespace TravioHotel.Models
{
    public class BookingRequest
    {

        [Key]
        public int id { get; set; }
        public Guid booking_code { get; set; }
        public int user_id { get; set; }
        public int from_country { get; set; }
        public string from_city { get;set; }
        public int to_country { get; set; }
        public string to_city { get;set; }
        public string departure_date { get; set; }    
        public string arrival_date { get; set; }
        public int status { get; set; } = 0;
        public int number_of_adults { get; set; }   
        public string? deleted_at { get; set; }
        public string? created_at { get; set; }

    }
}
