using Microsoft.EntityFrameworkCore;
using SystemGestionReservation.Core.Entities;
using SystemGestionReservation.Infrastructure.Data.Configurations;

namespace SystemGestionReservation.Infrastructure.Data;

public class SystemGestionReservationContext : DbContext
{
    //<SystemGestionReservationContext>
    public SystemGestionReservationContext(DbContextOptions options) : base(options) { }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Chambre> Chambres { get; set; }
    public DbSet<Equipement> Equipements { get; set; }
    public DbSet<Tarif> Tarifs { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Facture> Factures { get; set; }
    public DbSet<LigneFacture> LignesFacture { get; set; }
    public object Utilisateurs { get; internal set; }

    public SystemGestionReservationContext() : base(new
               DbContextOptionsBuilder<SystemGestionReservationContext>()

               .UseSqlServer(@"Data Source=.\SQLEXPRESS;Database=Hotel_MINIDB;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Application Name=""SQL Server Management Studio"";Command Timeout=0")
              .Options)
    { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ClientConfiguration());
        modelBuilder.ApplyConfiguration(new ChambreConfiguration());
        modelBuilder.ApplyConfiguration(new EquipementConfiguration());
        modelBuilder.ApplyConfiguration(new TarifConfiguration());
        modelBuilder.ApplyConfiguration(new ReservationConfiguration());
        modelBuilder.ApplyConfiguration(new FactureConfiguration());
        modelBuilder.ApplyConfiguration(new LigneFactureConfiguration());
        modelBuilder.ApplyConfiguration(new UtilisateurConfiguration());
    }
    public DbSet<Utilisateur> Utilisateur { get; set; }
   
}