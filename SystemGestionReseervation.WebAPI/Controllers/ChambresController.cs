using Microsoft.AspNetCore.Mvc;
using SystemGestionReservation.Application.DTOs.Chambre;
using SystemGestionReservation.Application.Interfaces;
using SystemGestionReservation.Core.Enums;

namespace SystemGestionReservation.Web.Controllers;

public class ChambresController : Controller
{
    private readonly IChambreService _chambreService;

    public ChambresController(IChambreService chambreService)
    {
        _chambreService = chambreService;
    }

    // GET: /Chambres
    public async Task<IActionResult> Index()
    {
        var chambres = await _chambreService.GetAllActiveAsync();
        return View(chambres);
    }

    // GET: /Chambres/Disponibles
    public async Task<IActionResult> Disponibles(DateTime dateArrivee, DateTime dateDepart, TypeChambre? type)
    {
        if (dateArrivee == default || dateDepart == default)
            return View(Enumerable.Empty<ChambreDto>());

        var chambres = await _chambreService.GetDisponiblesAsync(dateArrivee, dateDepart, type);
        ViewBag.DateArrivee = dateArrivee;
        ViewBag.DateDepart = dateDepart;
        return View(chambres);
    }

    // GET: /Chambres/Create
    public IActionResult Create() => View();

    // POST: /Chambres/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateChambreDto dto)
    {
        if (!ModelState.IsValid) return View(dto);

        await _chambreService.CreateAsync(dto);
        TempData["Success"] = "Chambre créée avec succès.";
        return RedirectToAction(nameof(Index));
    }

    // GET: /Chambres/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var chambre = await _chambreService.GetByIdAsync(id);
        if (chambre is null) return NotFound();

        var dto = new UpdateChambreDto
        {
            Type = chambre.Type,
            Etage = chambre.Etage,
            CapaciteAccueil = chambre.CapaciteAccueil,
            Description = chambre.Description
        };
        ViewBag.ChambreId = id;
        ViewBag.ChambreNumero = chambre.Numero;
        return View(dto);
    }

    // POST: /Chambres/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, UpdateChambreDto dto)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.ChambreId = id;
            return View(dto);
        }

        await _chambreService.UpdateAsync(id, dto);
        TempData["Success"] = "Chambre modifiée avec succès.";
        return RedirectToAction(nameof(Index));
    }

    // POST: /Chambres/AjouterEquipement
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AjouterEquipement(int chambreId, string nomEquipement)
    {
        await _chambreService.AjouterEquipementAsync(chambreId, nomEquipement);
        TempData["Success"] = "Équipement ajouté.";
        return RedirectToAction(nameof(Index));
    }

    // POST: /Chambres/Desactiver/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Desactiver(int id)
    {
        await _chambreService.DesactiverAsync(id);
        TempData["Success"] = "Chambre désactivée.";
        return RedirectToAction(nameof(Index));
    }
}