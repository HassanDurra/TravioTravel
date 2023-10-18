using System.ComponentModel.DataAnnotations;

namespace TravioHotel.Models
{
    public class Aircraft
    {
        [Key]
        public int Id { get; set; }
        public int agent_id { get; set; }
        public string aircraft_image { get; set; }
        public string aircraft_model_name { get; set; }
        public string aircraft_model_number { get; set; }
        public int total_seats { get; set; }
        public int remaining_seats { get; set; } = 0;
        public int bussiness_seats { get; set; } = 0;
        public int economy_seats { get; set; } = 0;
        public int first_class_seats { get; set; } = 0; 
        public int bussiness_seats_occupied { get; set; } = 0;
        public int economy_seats_occupied { get; set; } = 0;
        public int first_class_seats_occupied { get; set; } = 0;
        public int bussiness_seats_remaining { get; set; } = 0;
        public int economy_seats_remaining { get; set; } = 0;
        public int first_class_seats_remaining { get; set; } = 0;
        public int availibility { get; set; } = 0;
        
    }
}
