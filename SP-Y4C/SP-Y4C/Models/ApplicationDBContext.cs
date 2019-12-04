using Microsoft.EntityFrameworkCore;

namespace SP_Y4C.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Question> Questions { get; set; }
    }
}
