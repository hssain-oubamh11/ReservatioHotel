using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SystemGestionReservation.Core.Entities;

namespace SystemGestionReservation.Infrastructure.Data.Configurations;

public class EquipementConfiguration : IEntityTypeConfiguration<Equipement>
{
    public void Configure(EntityTypeBuilder<Equipement> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Nom)
               .IsRequired()
               .HasMaxLength(100);

        builder.ToTable("Equipements");
    }
}