using AT_API.Models;
using Microsoft.EntityFrameworkCore;

namespace AT_API.App_Code
{
    public class WorkshopAPI : DbContext
    {
        public WorkshopAPI(DbContextOptions<WorkshopAPI> options) : base(options) { }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
