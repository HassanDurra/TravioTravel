using System.ComponentModel.DataAnnotations;

namespace TravioHotel.Models
{
    public class Aircraft
    {
        [Key]
        public int Id { get; set; }
        public int agent_id { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string aircraft_image { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string aircraft_model_name { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string aircraft_model_number { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
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
