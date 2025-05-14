using Microsoft.EntityFrameworkCore;
using VisionHive.Infrastructure.Mappings;
using VisionHive.Infrastructure.Persistence;

namespace VisionHive.Infrastructure.Contexts
{
    public class AreaContext(DbContextOptions<AreaContext> options) : DbContext(options)
    {
        public DbSet<Area> Areas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AreaMapping());
            base.OnModelCreating(modelBuilder);
        }

    }
}
