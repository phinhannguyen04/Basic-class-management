using Microsoft.EntityFrameworkCore;
using my_app.Models.Entities;

namespace my_app.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options) 
        {
            
        }

        public DbSet<Student> Students { get; set; }
    }
}
