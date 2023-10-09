using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace TravioHotel.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string User_name { get; set; } 
        public string Email { get; set; }
        public string Password { get; set; }
        public int Role { get; set; } = 0;
        public string?  Image { get; set; }
        public string? created_at { get; set; }
        public string? email_verified_at { get; set; }
    }
}
