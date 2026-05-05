using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SystemGestionReservation.Core.Entities;

namespace SystemGestionReservation.Infrastructure.Data.Configurations;

public class TarifConfiguration : IEntityTypeConfiguration<Tarif>
{
    public void Configure(EntityTypeBuilder<Tarif> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.TypeChambre)
               .IsRequired()
               .HasConversion<string>();

        builder.Property(t => t.Saison)
               .IsRequired()
               .HasConversion<string>();

        builder.Property(t => t.PrixParNuit)
               .IsRequired()
               .HasColumnType("decimal(10,2)");

        // Un seul tarif par combinaison TypeChambre + Saison
        builder.HasIndex(t => new { t.TypeChambre, t.Saison })
               .IsUnique();

        builder.ToTable("Tarifs");
    }
}