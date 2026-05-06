using Microsoft.EntityFrameworkCore;
using SystemGestionReservation.Core.Entities;
using SystemGestionReservation.Core.Interfaces;
using SystemGestionReservation.Infrastructure.Data;

namespace SystemGestionReservation.Infrastructure.Repositories;

public class UtilisateurRepository : EfRepository<Utilisateur>, IUtilisateurRepository
{
    public UtilisateurRepository(SystemGestionReservationContext context) : base(context) { }

    public async Task<Utilisateur?> GetByLoginAsync(string login)
        => await _context.Utilisateur
                         .FirstOrDefaultAsync(u => u.Login == login);

    public async Task<IEnumerable<Utilisateur>> GetAllAsync()
        => await _context.Utilisateur
                         .OrderBy(u => u.Role)
                         .ThenBy(u => u.Login)
                         .ToListAsync();

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}