using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemGestionReservation.Application.DTOs.Reservation;
using SystemGestionReservation.Application.Interfaces;

namespace SystemGestionReservation.WebAPI.Controllers;

[Authorize(Roles = "Administrateur,Receptionniste")]
[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;
    private readonly IFactureService _factureService;

    public ReservationsController(
        IReservationService reservationService,
        IFactureService factureService)
    {
        _reservationService = reservationService;
        _factureService = factureService;
    }

    // GET: api/reservations/5
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var r = await _reservationService.GetByIdAsync(id);
        if (r is null)
            return NotFound(new { message = $"Réservation {id} introuvable." });
        return Ok(r);
    }

    // GET: api/reservations/client/5
    [HttpGet("client/{clientId:int}")]
    public async Task<IActionResult> GetByClient(int clientId)
        => Ok(await _reservationService.GetByClientIdAsync(clientId));

    // POST: api/reservations
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReservationDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try
        {
            var result = await _reservationService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // PUT: api/reservations/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id, [FromBody] UpdateReservationDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        try
        {
            await _reservationService.UpdateAsync(id, dto);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // POST: api/reservations/5/checkin
    [HttpPost("{id:int}/checkin")]
    public async Task<IActionResult> CheckIn(int id)
    {
        try
        {
            await _reservationService.CheckInAsync(id);
            return Ok(new { message = "Check-in effectué avec succès." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // POST: api/reservations/5/checkout
    [HttpPost("{id:int}/checkout")]
    public async Task<IActionResult> CheckOut(int id)
    {
        try
        {
            var facture = await _factureService.CheckOutEtGenererFactureAsync(id);
            return Ok(new
            {
                message = "Check-out effectué. Facture générée automatiquement.",
                factureId = facture.Id,
                montant = facture.MontantTotal
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // POST: api/reservations/5/annuler
    [HttpPost("{id:int}/annuler")]
    public async Task<IActionResult> Annuler(int id)
    {
        try
        {
            var (penalite, message) = await _reservationService.AnnulerAsync(id);
            return Ok(new
            {
                message,
                penaliteAppliquee = $"{penalite}%"
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // POST: api/reservations/5/remise
    [HttpPost("{id:int}/remise")]
    public async Task<IActionResult> AppliquerRemise(
        int id, [FromBody] decimal pourcentage)
    {
        try
        {
            await _reservationService.AppliquerRemiseAsync(id, pourcentage);
            return Ok(new { message = $"Remise de {pourcentage}% appliquée." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}