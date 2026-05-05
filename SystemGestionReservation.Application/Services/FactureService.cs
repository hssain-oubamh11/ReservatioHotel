using SystemGestionReservation.Application.DTOs.Facture;
using SystemGestionReservation.Application.Interfaces;
using SystemGestionReservation.Core.Entities;
using SystemGestionReservation.Core.Exceptions;
using SystemGestionReservation.Core.Interfaces;

namespace SystemGestionReservation.Application.Implementations;

public class FactureService : IFactureService
{
    private readonly IFactureRepository _factureRepository;
    private readonly IReservationRepository _reservationRepository;
    private readonly ITarifService _tarifService;

    public FactureService(IFactureRepository factureRepository,
                          IReservationRepository reservationRepository,
                          ITarifService tarifService)
    {
        _factureRepository = factureRepository;
        _reservationRepository = reservationRepository;
        _tarifService = tarifService;
    }

    public async Task<FactureDto?> GetByIdAsync(int id)
    {
        var facture = await _factureRepository.GetByIdAsync(id);
        return facture is null ? null : ToDto(facture);
    }

    public async Task<FactureDto?> GetByReservationIdAsync(int reservationId)
    {
        var facture = await _factureRepository.GetByReservationIdAsync(reservationId);
        return facture is null ? null : ToDto(facture);
    }

    public async Task<IEnumerable<FactureDto>> GetByClientIdAsync(int clientId)
    {
        var factures = await _factureRepository.GetByClientIdAsync(clientId);
        return factures.Select(ToDto);
    }

    public async Task<FactureDto> GenererFactureAsync(int reservationId)
    {
        var reservation = await _reservationRepository.GetByIdAsync(reservationId)
            ?? throw new ReservationIntrouvableException(reservationId);

        // Vérifier qu'une facture n'existe pas déjà
        var existante = await _factureRepository.GetByReservationIdAsync(reservationId);
        if (existante is not null)
            return ToDto(existante);

        // Calculer le prix par nuit selon le tarif de la saison
        var prixParNuit = await _tarifService.GetPrixParNuitAsync(
            reservation.Chambre!.Type, reservation.DateArrivee);

        // Créer les lignes de facture
        var lignes = new List<LigneFacture>
        {
            new LigneFacture(
                $"Nuitées - Chambre {reservation.Chambre.Numero} ({reservation.Chambre.Type})",
                reservation.NombreNuits,
                prixParNuit)
        };

        var remise = reservation.RemiseAppliquee ?? 0;
        var facture = new Facture(reservationId, lignes, remise);

        await _factureRepository.AddAsync(facture);
        await _factureRepository.SaveChangesAsync();
        return ToDto(facture);
    }

    // ── Mapping manuel ──────────────────────────────────────────────
    private static FactureDto ToDto(Facture f) => new()
    {
        Id = f.Id,
        ReservationId = f.ReservationId,
        ClientNomComplet = f.Reservation?.Client is not null
                            ? $"{f.Reservation.Client.Prenom} {f.Reservation.Client.Nom}"
                            : string.Empty,
        ChambreNumero = f.Reservation?.Chambre?.Numero ?? string.Empty,
        DateArrivee = f.Reservation?.DateArrivee ?? default,
        DateDepart = f.Reservation?.DateDepart ?? default,
        NombreNuits = f.Reservation?.NombreNuits ?? 0,
        DateEmission = f.DateEmission,
        MontantTotal = f.MontantTotal,
        EstPayee = f.EstPayee,
        Lignes = f.Lignes.Select(l => new LigneFactureDto
        {
            Description = l.Description,
            Quantite = l.Quantite,
            PrixUnitaire = l.PrixUnitaire,
            SousTotal = l.SousTotal
        }).ToList()
    };

    public Task<FactureDto> CheckOutEtGenererFactureAsync(int reservationId)
    {
        throw new NotImplementedException();
    }
}