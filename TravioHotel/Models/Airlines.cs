using System.ComponentModel.DataAnnotations;

namespace TravioHotel.Models
{
    public class Airlines
    {
        [Key]
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string AirlineImage { get; set; }
        public string Airlinename { get; set; }
        public string IATACode { get; set; }    
        public string ICAOCode { get; set; }    
        public string Headquarters { get; set; }
    }
}
