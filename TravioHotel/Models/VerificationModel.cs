using System.ComponentModel.DataAnnotations;

namespace TravioHotel.Models
{
    public class VerificationModel
    {
        [Key]
        public int Id { get; set; }
        public string Verification_email { get; set; }  
        public string Verification_code { get; set; }   
    }
}
