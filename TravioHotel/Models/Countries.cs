using System.ComponentModel.DataAnnotations;

namespace TravioHotel.Models
{
    public class Countries
    {
        [Key]
        public int id { get; set; } 
        public string name { get; set; }    
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
