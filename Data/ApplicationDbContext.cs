using GymManagementSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Member> Members { get; set; }

        public DbSet<Package> Packages { get; set; }
    }
}
