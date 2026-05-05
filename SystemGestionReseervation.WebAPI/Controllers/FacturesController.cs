using Microsoft.AspNetCore.Mvc;
using SystemGestionReservation.Application.Interfaces;

namespace SystemGestionReservation.Web.Controllers;

public class FacturesController : Controller
{
    private readonly IFactureService _factureService;

    public FacturesController(IFactureService factureService)
    {
        _factureService = factureService;
    }

    // GET: /Factures/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var facture = await _factureService.GetByIdAsync(id);
        if (facture is null) return NotFound();
        return View(facture);
    }

    // GET: /Factures/ParClient/5
    public async Task<IActionResult> ParClient(int clientId)
    {
        var factures = await _factureService.GetByClientIdAsync(clientId);
        ViewBag.ClientId = clientId;
        return View(factures);
    }
}