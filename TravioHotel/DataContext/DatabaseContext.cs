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
   
        public DbSet<VerificationModel> Verification {  get ; set; } 
    
    }
}
