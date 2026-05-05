using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SystemGestionReservation.Core.Entities;

namespace SystemGestionReservation.Infrastructure.Data.Configurations;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Nom)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(c => c.Prenom)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(c => c.NumeroIdentite)
               .IsRequired()
               .HasMaxLength(50);

        builder.HasIndex(c => c.NumeroIdentite)
               .IsUnique();

        builder.Property(c => c.Adresse)
               .HasMaxLength(250);

        builder.Property(c => c.Telephone)
               .HasMaxLength(20);

        builder.Property(c => c.Email)
               .HasMaxLength(150);

        builder.Property(c => c.EstActif)
               .IsRequired()
               .HasDefaultValue(true);

        builder.HasMany(c => c.Reservations)
               .WithOne(r => r.Client)
               .HasForeignKey(r => r.ClientId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.ToTable("Clients");
    }
}