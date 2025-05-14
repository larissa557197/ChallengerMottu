using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VisionHive.Infrastructure.Persistence;

namespace VisionHive.Infrastructure.Mappings
{
    public class AreaMapping : IEntityTypeConfiguration<Area>
    {
        public void Configure(EntityTypeBuilder<Area> builder)
        {
            builder
                .ToTable("AREA");

            builder
                .HasKey(a => a.Id);

            builder
                .Property(a => a.Id)
                .HasColumnName("ID")
                .HasColumnType("RAW(16)")
                .HasConversion(
                    v => v.ToByteArray(),
                    v => new Guid(v)
                )
                .IsRequired();

            builder
                .Property(a => a.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(a => a.DateCreated)
                .HasColumnName("DATE_CREATED")
                .IsRequired()
                .HasColumnType("DATE");

            builder
                .Property(a => a.DateModified)
                .HasColumnName("DATE_MODIFIED")
                .IsRequired()
                .HasColumnType("DATE");

            builder
                .Property(a => a.Status)
                .HasColumnName("STATUS")
                .IsRequired()
                .HasConversion<int>();

            // Relacionamento: Uma Area TEM MUITAS Motos
            builder
                .HasMany(a => a.Motos)
                .WithOne(m => m.Area)
                .HasForeignKey(m => m.AreaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
