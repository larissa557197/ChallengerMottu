using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VisionHive.Infrastructure.Persistence;

namespace VisionHive.Infrastructure.Mappings
{
    public class MotoMapping : IEntityTypeConfiguration<Moto>
    {
        public void Configure(EntityTypeBuilder<Moto> builder)
        {
            builder
                .ToTable("MOTOS");

            builder
                .HasKey(m => m.Id);

            builder
                .Property(m => m.Id)
                .HasColumnName("ID")
                .HasColumnType("RAW(16)")
                .HasConversion(
                    v => v.ToByteArray(),
                    v => new Guid(v)
                )
                .IsRequired();

            builder
                .Property(m => m.Placa)
                .HasColumnName("PLACA")
                .HasMaxLength(10)
                .IsRequired();

            builder
                .Property(m => m.Chassi)
                .HasColumnName("CHASSI")
                .HasMaxLength(20)
                .IsRequired();

            builder
                .Property(m => m.EstaComLote)
                .HasColumnName("ESTA_COM_LOTE")
                .HasColumnType("NUMBER(1)")
                .HasConversion<int>()
                .IsRequired();

            builder
                .Property(m => m.Categoria)
                .HasColumnName("CATEGORIA")
                .HasConversion<string>()
                .IsRequired();

            builder
                .Property(m => m.DateCreated)
                .HasColumnName("DATE_CREATED")
                .HasColumnType("DATE")
                .IsRequired();

            builder
                .Property(m => m.DateModified)
                .HasColumnName("DATE_MODIFIED")
                .HasColumnType("DATE")
                .IsRequired();

            builder
                .Property(m => m.Status)
                .HasColumnName("STATUS")
                .HasConversion<int>()
                .IsRequired();

            // Relacionamento com Area
            builder
                .Property(m => m.AreaId)
                .HasColumnName("AREA_ID")
                .HasColumnType("RAW(16)")
                .HasConversion(
                    v => v.ToByteArray(),
                    v => new Guid(v)
                )
                .IsRequired();

            builder
                .HasOne(m => m.Area)
                .WithMany(a => a.Motos)
                .HasForeignKey(m => m.AreaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

