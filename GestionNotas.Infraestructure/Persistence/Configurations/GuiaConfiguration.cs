using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GestionNotas.Api.Domain.Entities;

namespace GestionNotas.Api.Infrastructure.Persistence.Configurations;

public class GuiaConfiguration : IEntityTypeConfiguration<Guia>
{
    public void Configure(EntityTypeBuilder<Guia> builder)
    {
        builder.ToTable("Guias");

        builder.HasKey(g => g.Id);

        builder.Property(g => g.Id)
            .UseIdentityColumn();

        builder.Property(g => g.NumeroGuia)
            .HasMaxLength(20)
            .IsRequired();

        builder.HasIndex(g => g.NumeroGuia)
            .IsUnique();

        builder.Property(g => g.Remitente)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(g => g.Destinatario)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(g => g.CiudadOrigen)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(g => g.CiudadDestino)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(g => g.PesoKg)
            .HasPrecision(10, 3)
            .IsRequired();

        builder.Property(g => g.Estado)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(g => g.FechaCreacion)
            .IsRequired();

        builder.Property(g => g.FechaEntrega)
            .IsRequired(false);
    }
}
