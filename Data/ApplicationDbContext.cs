using Microsoft.EntityFrameworkCore;
using PopulationChecker.WorkerService.Models;

namespace PopulationChecker.WorkerService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CitizenInfo> Citizens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
