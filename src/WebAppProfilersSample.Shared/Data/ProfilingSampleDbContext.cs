using Microsoft.EntityFrameworkCore;
using WebAppProfilersSample.Shared.Data.Configurations;
using WebAppProfilersSample.Shared.Data.Entities;

namespace WebAppProfilersSample.Shared.Data
{
    public class ProfilingSampleDbContext : DbContext
    {
        public DbSet<SomeEntity> Somes { get; set; }
        public DbSet<SomeOtherEntity> Others { get; set; }

        public ProfilingSampleDbContext(DbContextOptions<ProfilingSampleDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.ApplyConfiguration(new SomeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SomeOtherEntityConfiguration());
        }
    }
}
