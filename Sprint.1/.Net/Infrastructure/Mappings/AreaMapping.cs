using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VisionHive.Infrastructure.Persistence;

namespace VisionHive.Infrastructure.Mappings
{
    public class AreaMapping: IEntityTypeConfiguration<Area>
    {
        public void Configure(EntityTypeBuilder<Area> builder)
        {
            builder
                .ToTable("Areas");
            builder
                .HasKey(a => a.Id);
            builder
                .Property(a => a.Nome)
                .HasMaxLength(50);
            builder
                .Property(a => a.DateCreated)
                .IsRequired()
                .HasColumnType("DATE");
            builder
                .Property(a => a.DateModified)
                .IsRequired()
                .HasColumnType("DATE");
            builder
                .Property(a => a.Status)
                .IsRequired()
                .HasConversion<int>();
        }
    }
    
}

