using Org.BouncyCastle.Asn1.Cms;
using System.ComponentModel.DataAnnotations;

namespace TravioHotel.Models
{
    public class BookingFlightDetails
    {

        [Key]
        public int id { get; set; }
        public string ? airline_image { get; set; }
        public string ? airline_name { get; set; }
        public string ? air_craft_id { get; set; }
        public int journey_type { get; set; } = 0; // 0 Oneway and 1 Two Way 
        public string from { get;set; }
        public int to { get; set; }
        public string departure_date { get;set; }
        public string departure_time { get; set; }
        public string arrival_time { get; set; }
        public string arrival_date { get; set; }    
        public string flight_duration { get; set; }
        public string class_type { get; set; }
        public int total_price { get; set; } = 0;   
        public string? deleted_at { get; set; }
        public string? created_at { get; set; }

    }
}
