using System.ComponentModel.DataAnnotations;

namespace TravioHotel.Models
{
    public class BookingFlightClientDetails
    {
        [Key]
        public int id { get; set; }
        public int flight_details_id { get; set; }    
        public string image { get; set; }
        public string firstName { get; set; }
        public string? lastName { get; set; }
        public string email { get; set; }
        public string passport_number { get; set; }
        public string? Cnic_number { get; set; }   
        public string contact_number { get; set; } 
        public string age { get; set; }    
        public string date_of_birth { get; set; }   
        public string country_name { get; set; }    
        public string city_name { get; set ; }
        public int is_booked { get; set; } = 0;
        public int status { get; set; } = 0;
        public string Booking_Number { get; set; }
        public string? created_at { get; set; }

        public string payment_method { get; set; }
    }
}
