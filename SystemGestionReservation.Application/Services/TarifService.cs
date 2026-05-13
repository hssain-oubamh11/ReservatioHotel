using SystemGestionReservation.Application.DTOs.Tarif;
using SystemGestionReservation.Application.Interfaces;
using SystemGestionReservation.Core.Entities;
using SystemGestionReservation.Core.Enums;
using SystemGestionReservation.Core.Interfaces;

namespace SystemGestionReservation.Application.Implementations;

public class TarifService : ITarifService
{
    private readonly ITarifRepository _tarifRepository;

    public TarifService(ITarifRepository tarifRepository)
    {
        _tarifRepository = tarifRepository;
    }

    public async Task<IEnumerable<TarifDto>> GetAllAsync()
    {
        var tarifs = await _tarifRepository.GetAllAsync();
        return tarifs.Select(ToDto);
    }

    public async Task<TarifDto> CreateAsync(CreateTarifDto dto)
    {
        var tarif = new Tarif(dto.TypeChambre, dto.Saison, dto.PrixParNuit);
        await _tarifRepository.AddAsync(tarif);
        await _tarifRepository.SaveChangesAsync();
        return ToDto(tarif);
    }

    public async Task<decimal> GetPrixParNuitAsync(TypeChambre typeChambre, DateTime date)
    {
        // Règle métier : déterminer la saison selon la date
        var saison = DeterminerSaison(date);
        var tarif = await _tarifRepository.GetByTypeSaisonAsync(typeChambre, saison)
            ?? throw new KeyNotFoundException($"Aucun tarif défini pour {typeChambre} en {saison}.");
        return tarif.PrixParNuit;
    }

    public async Task UpdateAsync(int id, UpdateTarifDto dto)
    {
        var tarif = await _tarifRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Tarif {id} introuvable.");

        tarif.ModifierPrix(dto.PrixParNuit);
        await _tarifRepository.UpdateAsync(tarif);
        await _tarifRepository.SaveChangesAsync();
    }

    // ── Règle métier : saison selon le mois ─────────────────────────
    private static Saison DeterminerSaison(DateTime date) => date.Month switch
    {
        6 or 7 or 8 => Saison.HauteSaison,
        12 or 1 => Saison.PeriodeSpeciale,
        _ => Saison.BasseSaison
    };

    private static TarifDto ToDto(Tarif t) => new()
    {
        Id = t.Id,
        TypeChambre = t.TypeChambre,
        Saison = t.Saison,
        PrixParNuit = t.PrixParNuit
    };
}