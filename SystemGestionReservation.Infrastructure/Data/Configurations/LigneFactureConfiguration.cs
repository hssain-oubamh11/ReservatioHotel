using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SystemGestionReservation.Core.Entities;

namespace SystemGestionReservation.Infrastructure.Data.Configurations;

public class LigneFactureConfiguration : IEntityTypeConfiguration<LigneFacture>
{
    public void Configure(EntityTypeBuilder<LigneFacture> builder)
    {
        builder.HasKey(l => l.Id);

        builder.Property(l => l.FactureId)
               .IsRequired();

        builder.Property(l => l.Description)
               .IsRequired()
               .HasMaxLength(250);

        builder.Property(l => l.Quantite)
               .IsRequired();

        builder.Property(l => l.PrixUnitaire)
               .IsRequired()
               .HasColumnType("decimal(10,2)");

        // SousTotal est calculé → ignoré par EF
        builder.Ignore(l => l.SousTotal);

        builder.ToTable("LignesFacture");
    }
}