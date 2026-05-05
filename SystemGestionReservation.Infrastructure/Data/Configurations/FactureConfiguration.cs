using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SystemGestionReservation.Core.Entities;

namespace SystemGestionReservation.Infrastructure.Data.Configurations;

public class FactureConfiguration : IEntityTypeConfiguration<Facture>
{
    public void Configure(EntityTypeBuilder<Facture> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(f => f.ReservationId)
               .IsRequired();

        builder.Property(f => f.DateEmission)
               .IsRequired();

        builder.Property(f => f.MontantTotal)
               .IsRequired()
               .HasColumnType("decimal(10,2)");

        builder.Property(f => f.EstPayee)
               .IsRequired()
               .HasDefaultValue(false);

        builder.HasMany(f => f.Lignes)
               .WithOne()
               .HasForeignKey(l => l.FactureId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("Factures");
    }
}