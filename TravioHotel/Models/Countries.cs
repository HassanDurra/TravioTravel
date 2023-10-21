using System.ComponentModel.DataAnnotations;

namespace TravioHotel.Models
{
    public class Countries
    {
        [Key]
        public int id { get; set; } 
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string name { get; set; }    
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string ? iso3 { get; set; }
        public string? iso2 { get; set; }
        public string? phonecode { get; set; }
        public string?capital { get; set; }
        public string? currency { get; set; }
        public string? currency_symbol { get; set; }
       public string? tld { get; set; }
       [StringLength(5000)]
       public string ? timezone { get; set; }
       public string ? native { get; set; }
       public string? emoji { get; set; }
       public string? region { get; set; }
       public string ? subregion { get; set; }
    }
}
