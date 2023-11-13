using System.ComponentModel.DataAnnotations;

namespace TravioHotel.Models
{
    public class Contact
    {
        [Key] public int Id { get; set; }
        public int user_id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string subject { get; set; }
        [StringLength(1200)]
        public string message { get; set; }
        public int is_view { get; set; } = 0;
        public int is_replied { get; set; } = 0;
        public string created_at { get; set; } 



    }
}
