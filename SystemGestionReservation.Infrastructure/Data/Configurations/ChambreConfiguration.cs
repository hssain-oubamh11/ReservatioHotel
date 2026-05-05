using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SystemGestionReservation.Core.Entities;

namespace SystemGestionReservation.Infrastructure.Data.Configurations;

public class ChambreConfiguration : IEntityTypeConfiguration<Chambre>
{
    public void Configure(EntityTypeBuilder<Chambre> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Numero)
               .IsRequired()
               .HasMaxLength(10);

        builder.HasIndex(c => c.Numero)
               .IsUnique();

        builder.Property(c => c.Type)
               .IsRequired()
               .HasConversion<string>();

        builder.Property(c => c.Etage)
               .IsRequired();

        builder.Property(c => c.CapaciteAccueil)
               .IsRequired();

        builder.Property(c => c.Description)
               .HasMaxLength(500);

        builder.Property(c => c.EstActive)
               .IsRequired()
               .HasDefaultValue(true);

        // Relation Many-to-Many avec Equipement
        builder.HasMany(c => c.Equipements)
               .WithMany()
               .UsingEntity(j => j.ToTable("ChambreEquipements"));

        builder.HasMany(c => c.Reservations)
               .WithOne(r => r.Chambre)
               .HasForeignKey(r => r.ChambreId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable("Chambres");
    }
}