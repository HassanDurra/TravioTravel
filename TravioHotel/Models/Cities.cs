using System.ComponentModel.DataAnnotations;

namespace TravioHotel.Models
{
    public class Cities
    {
        [Key]
        public int Id { get; set; }
        public string name { get; set; }
        public int country_id { get; set; }
        public string country_code { get; set; }
        public string state_code { get; set; }
        public int state_id { get; set; }

    }
}
