using SystemGestionReservation.Application.DTOs.Reservation;

namespace SystemGestionReservation.Application.Interfaces;

public interface IReservationService
{
    Task<ReservationDto?> GetByIdAsync(int id);
    Task<IEnumerable<ReservationDto>> GetByClientIdAsync(int clientId);
    Task<ReservationDto> CreateAsync(CreateReservationDto dto);
    Task UpdateAsync(int id, UpdateReservationDto dto);
    Task AnnulerAsync(int id);
    Task CheckInAsync(int id);
    Task AppliquerRemiseAsync(int id, decimal pourcentage);
}