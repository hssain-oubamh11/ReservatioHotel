using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SystemGestionReservation.Application.Interfaces;

namespace SystemGestionReservation.Web.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class FacturesController : ControllerBase
{
    private readonly IFactureService _factureService;

    public FacturesController(IFactureService factureService)
    {
        _factureService = factureService;
    }

    // GET: api/factures/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var facture = await _factureService.GetByIdAsync(id);

        if (facture == null)
            return NotFound();

        return Ok(facture);
    }

    // GET: api/factures/client/5
    [HttpGet("client/{clientId}")]
    public async Task<IActionResult> ParClient(int clientId)
    {
        var factures = await _factureService.GetByClientIdAsync(clientId);

        return Ok(factures);
    }
}