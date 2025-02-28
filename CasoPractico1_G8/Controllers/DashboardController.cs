using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CasoPractico1_G8.Models;
using Microsoft.AspNetCore.Authorization;

[Authorize(Roles = "Admin")]
public class DashboardController : Controller
{
    private readonly CasoPractico1_G8Context _context;

    public DashboardController(CasoPractico1_G8Context context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var totalRutasActivas = await _context.Ruta.CountAsync(r => r.Estado == true);
        var totalVehiculosBuenos = await _context.Vehiculo.CountAsync(v => v.Estado == "Bueno");
        var boletosVendidos = await _context.Boleto.CountAsync();

        ViewBag.TotalRutasActivas = totalRutasActivas;
        ViewBag.TotalVehiculosBuenos = totalVehiculosBuenos;
        ViewBag.BoletosVendidos = boletosVendidos;

        return View();
    }
}

