using System.ComponentModel.DataAnnotations;

namespace TravioHotel.Models
{
    public class Airport
    {
        [Key]
        public int    Id { get; set; }

        public string Name { get; set; }    
        public string Country_iso { get; set; }
        public string? City_name { get; set; }
        public string IataCode { get; set; }
        public string ? delete_at { get; set; }
        public string ? IcaoCode { get; set; }

     

    }
}
