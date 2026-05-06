using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SystemGestionReservation.Core.Entities;

namespace SystemGestionReservation.Infrastructure.Data.Configurations;

public class UtilisateurConfiguration : IEntityTypeConfiguration<Utilisateur>
{
    public void Configure(EntityTypeBuilder<Utilisateur> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Login)
               .IsRequired()
               .HasMaxLength(100);

        builder.HasIndex(u => u.Login)
               .IsUnique();

        builder.Property(u => u.PasswordHash)
               .IsRequired();

        builder.Property(u => u.Role)
               .IsRequired()
               .HasConversion<string>();

        builder.Property(u => u.EstActif)
               .IsRequired()
               .HasDefaultValue(true);

        builder.HasOne(u => u.Client)
               .WithMany()
               .HasForeignKey(u => u.ClientId)
               .IsRequired(false)
               .OnDelete(DeleteBehavior.SetNull);

        builder.ToTable("Utilisateurs");
    }
}