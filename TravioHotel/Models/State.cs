using System.ComponentModel.DataAnnotations;

namespace TravioHotel.Models
{
    public class State
    {
        [Key]
        public int Id { get; set; }
        public int? country_id { get; set; }
        public string? country_code { get; set; }
        public string? name { get; set; }
        public string? iso2 { get; set; }
        public string? fips { get; set; }


    }
}
