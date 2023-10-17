using System.ComponentModel.DataAnnotations;

namespace TravioHotel.Models
{
    public class Service_account
    {
        [Key]
       public  int Id { get; set; }    
       public  int serviceId { get; set; } 
       public  int userId { get; set; }
    }
}
