using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SystemGestionReservation.Core.Entities;

namespace SystemGestionReservation.Infrastructure.Data.Configurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.ClientId)
               .IsRequired();

        builder.Property(r => r.ChambreId)
               .IsRequired();

        builder.Property(r => r.DateArrivee)
               .IsRequired();

        builder.Property(r => r.DateDepart)
               .IsRequired();

        builder.Property(r => r.NombrePersonnes)
               .IsRequired();

        builder.Property(r => r.Statut)
               .IsRequired()
               .HasConversion<string>()
               .HasMaxLength(30);

        builder.Property(r => r.DateCreation)
               .IsRequired();

        builder.Property(r => r.HeureArriveeEffective)
               .IsRequired(false);

        builder.Property(r => r.RemiseAppliquee)
               .HasColumnType("decimal(5,2)")
               .IsRequired(false);

        builder.HasOne(r => r.Facture)
               .WithOne(f => f.Reservation)
               .HasForeignKey<Facture>(f => f.ReservationId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.ToTable("Reservations");
    }
}