using Microsoft.EntityFrameworkCore;
using SystemGestionReservation.Core.Entities;
using SystemGestionReservation.Core.Interfaces;
using SystemGestionReservation.Infrastructure.Data;

namespace SystemGestionReservation.Infrastructure.Repositories;

public class ReservationRepository : EfRepository<Reservation>, IReservationRepository
{
    public ReservationRepository(SystemGestionReservationContext context) : base(context) { }

    public new async Task<Reservation?> GetByIdAsync(int id)
        => await _context.Reservations
                         .Include(r => r.Client)
                         .Include(r => r.Chambre)
                         .Include(r => r.Facture)
                         .FirstOrDefaultAsync(r => r.Id == id);

    public async Task<IEnumerable<Reservation>> GetByClientIdAsync(int clientId)
        => await _context.Reservations
                         .Include(r => r.Chambre)
                         .Include(r => r.Facture)
                         .Where(r => r.ClientId == clientId)
                         .OrderByDescending(r => r.DateCreation)
                         .ToListAsync();

    public async Task<IEnumerable<Reservation>> GetByChambreIdAsync(int chambreId)
        => await _context.Reservations
                         .Include(r => r.Client)
                         .Where(r => r.ChambreId == chambreId)
                         .OrderByDescending(r => r.DateArrivee)
                         .ToListAsync();

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}