using System.ComponentModel.DataAnnotations;

namespace TravioHotel.Models
{
    public class Countries
    {
        [Key]
        public int id { get; set; } 
        public string name { get; set; }    
    }
}
