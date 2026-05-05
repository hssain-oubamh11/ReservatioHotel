using SystemGestionReservation.Application.DTOs.Facture;

namespace SystemGestionReservation.Application.Interfaces;

public interface IFactureService
{
    Task<FactureDto?> GetByIdAsync(int id);
    Task<FactureDto?> GetByReservationIdAsync(int reservationId);
    Task<IEnumerable<FactureDto>> GetByClientIdAsync(int clientId);
    Task<FactureDto> CheckOutEtGenererFactureAsync(int reservationId);
}