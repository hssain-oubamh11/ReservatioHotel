using SystemGestionReservation.Core.Entities;
using SystemGestionReservation.Core.Enums;

namespace SystemGestionReservation.Core.Interfaces;

// Interface du service principal — conforme au cours Couche service
public interface IGestionHotelService
{
    // ── Clients (énoncé §3.1) ─────────────────────────────────────
    Task<IEnumerable<Client>> GetAllClientsAsync();
    Task<Client?> GetClientByIdAsync(int id);
    Task<IEnumerable<Client>> SearchClientsAsync(string terme);
    Task<Client> AddClientAsync(Client client);
    Task UpdateClientAsync(Client client);
    Task DesactiverClientAsync(int id);

    // ── Chambres (énoncé §3.2) ────────────────────────────────────
    Task<IEnumerable<Chambre>> GetAllChambresAsync();
    Task<IEnumerable<Chambre>> GetChambresDisponiblesAsync(DateTime dateArrivee,
                                                            DateTime dateDepart,
                                                            TypeChambre? type = null);
    Task<Chambre> AddChambreAsync(Chambre chambre);
    Task UpdateChambreAsync(Chambre chambre);
    Task AjouterEquipementAsync(int chambreId, string nomEquipement);
    Task DesactiverChambreAsync(int id);

    // ── Tarifs (énoncé §3.3) ──────────────────────────────────────
    Task<IEnumerable<Tarif>> GetAllTarifsAsync();
    Task<Tarif> AddTarifAsync(Tarif tarif);
    Task<decimal> GetPrixParNuitAsync(TypeChambre type, DateTime date);

    // ── Réservations (énoncé §3.4) ────────────────────────────────
    Task<Reservation?> GetReservationByIdAsync(int id);
    Task<IEnumerable<Reservation>> GetReservationsByClientAsync(int clientId);
    Task<Reservation> AddReservationAsync(Reservation reservation);
    Task UpdateReservationAsync(Reservation reservation);
    Task AnnulerReservationAsync(int id);
    Task CheckInAsync(int id);
    Task AppliquerRemiseAsync(int id, decimal pourcentage);

    // ── Factures (énoncé §3.5) ────────────────────────────────────
    Task<Facture?> GetFactureByIdAsync(int id);
    Task<IEnumerable<Facture>> GetFacturesByClientAsync(int clientId);
    Task<Facture> CheckOutEtGenererFactureAsync(int reservationId);
}