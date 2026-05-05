using SystemGestionReservation.Application.DTOs.Reservation;
using SystemGestionReservation.Application.Interfaces;
using SystemGestionReservation.Core.Entities;
using SystemGestionReservation.Core.Exceptions;
using SystemGestionReservation.Core.Interfaces;

namespace SystemGestionReservation.Application.Implementations;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IChambreRepository _chambreRepository;
    private readonly IClientRepository _clientRepository;

    public ReservationService(IReservationRepository reservationRepository,
                               IChambreRepository chambreRepository,
                               IClientRepository clientRepository)
    {
        _reservationRepository = reservationRepository;
        _chambreRepository = chambreRepository;
        _clientRepository = clientRepository;
    }

    public async Task<IEnumerable<ReservationDto>> GetByClientIdAsync(int clientId)
    {
        var reservations = await _reservationRepository.GetByClientIdAsync(clientId);
        return reservations.Select(ToDto);
    }

    public async Task<ReservationDto?> GetByIdAsync(int id)
    {
        var reservation = await _reservationRepository.GetByIdAsync(id);
        return reservation is null ? null : ToDto(reservation);
    }

    public async Task<ReservationDto> CreateAsync(CreateReservationDto dto)
    {
        // Vérifier disponibilité de la chambre
        var disponibles = await _chambreRepository.GetDisponiblesAsync(dto.DateArrivee, dto.DateDepart);
        var chambre = disponibles.FirstOrDefault(c => c.Id == dto.ChambreId)
            ?? throw new ChambreIndisponibleException(dto.ChambreId.ToString());

        // Vérifier que le client existe
        var client = await _clientRepository.GetByIdAsync(dto.ClientId)
            ?? throw new KeyNotFoundException($"Client {dto.ClientId} introuvable.");

        if (!client.EstActif)
            throw new InvalidOperationException("Ce client est désactivé.");

        var reservation = new Reservation(dto.ClientId, dto.ChambreId,
                                          dto.DateArrivee, dto.DateDepart,
                                          dto.NombrePersonnes);
        reservation.Confirmer();
        await _reservationRepository.AddAsync(reservation);
        await _reservationRepository.SaveChangesAsync();
        return ToDto(reservation);
    }

    public async Task UpdateAsync(int id, UpdateReservationDto dto)
    {
        var reservation = await _reservationRepository.GetByIdAsync(id)
            ?? throw new ReservationIntrouvableException(id);

        // Vérifier disponibilité de la nouvelle chambre
        var disponibles = await _chambreRepository.GetDisponiblesAsync(dto.DateArrivee, dto.DateDepart);
        bool chambreDisponible = disponibles.Any(c => c.Id == dto.ChambreId)
                                 || dto.ChambreId == reservation.ChambreId;
        if (!chambreDisponible)
            throw new ChambreIndisponibleException(dto.ChambreId.ToString());

        reservation.Modifier(dto.DateArrivee, dto.DateDepart, dto.ChambreId, dto.NombrePersonnes);
        await _reservationRepository.UpdateAsync(reservation);
        await _reservationRepository.SaveChangesAsync();
    }

    public async Task AnnulerAsync(int id)
    {
        var reservation = await _reservationRepository.GetByIdAsync(id)
            ?? throw new ReservationIntrouvableException(id);

        reservation.Annuler();
        await _reservationRepository.UpdateAsync(reservation);
        await _reservationRepository.SaveChangesAsync();
    }

    public async Task CheckInAsync(int id)
    {
        var reservation = await _reservationRepository.GetByIdAsync(id)
            ?? throw new ReservationIntrouvableException(id);

        reservation.EffectuerCheckIn();
        await _reservationRepository.UpdateAsync(reservation);
        await _reservationRepository.SaveChangesAsync();
    }

    public async Task CheckOutAsync(int id)
    {
        var reservation = await _reservationRepository.GetByIdAsync(id)
            ?? throw new ReservationIntrouvableException(id);

        reservation.EffectuerCheckOut();
        await _reservationRepository.UpdateAsync(reservation);
        await _reservationRepository.SaveChangesAsync();
    }

    public async Task AppliquerRemiseAsync(int id, decimal pourcentage)
    {
        var reservation = await _reservationRepository.GetByIdAsync(id)
            ?? throw new ReservationIntrouvableException(id);

        reservation.AppliquerRemise(pourcentage);
        await _reservationRepository.UpdateAsync(reservation);
        await _reservationRepository.SaveChangesAsync();
    }

    //Mapping manuel
    private static ReservationDto ToDto(Reservation r) => new()
    {
        Id = r.Id,
        ClientId = r.ClientId,
        ClientNomComplet = r.Client is not null ? $"{r.Client.Prenom} {r.Client.Nom}" : string.Empty,
        ChambreId = r.ChambreId,
        ChambreNumero = r.Chambre?.Numero ?? string.Empty,
        DateArrivee = r.DateArrivee,
        DateDepart = r.DateDepart,
        NombrePersonnes = r.NombrePersonnes,
        NombreNuits = r.NombreNuits,
        Statut = r.Statut,
        DateCreation = r.DateCreation,
        RemiseAppliquee = r.RemiseAppliquee
    };
}