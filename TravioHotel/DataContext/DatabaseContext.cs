using Microsoft.EntityFrameworkCore;
using TravioHotel.Models;

namespace TravioHotel.DataContext
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext (DbContextOptions option) : base(option){}

        public DbSet<UserModel> User { get ; set; }  
        public DbSet<Countries> Countries { get ; set; }  
        public DbSet<Airlines> Airlines { get ; set; }
        public DbSet<Airport> Airports { get ; set; }
        public DbSet<Cities> Cities { get ; set; }
        public DbSet<State> State { get ; set; }
        public DbSet<Aircraft>Aircrafts { get; set; }
        public DbSet<Service_account> Service_Account { get ; set; }
        public DbSet<VerificationModel> Verification {  get ; set; } 
    
    }
}
