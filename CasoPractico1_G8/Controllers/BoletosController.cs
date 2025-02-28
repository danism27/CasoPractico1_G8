using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CasoPractico1_G8.Models;

public class BoletosController : Controller
{
    private readonly CasoPractico1_G8Context _context;

    public BoletosController(CasoPractico1_G8Context context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Boleto.ToListAsync());
    }

    public IActionResult Comprar()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Comprar(Boleto boleto)
    {
        if (ModelState.IsValid)
        {
            var ruta = await _context.Ruta.FindAsync(boleto.RutaId);
            var vehiculo = await _context.Vehiculo.FindAsync(ruta.VehiculoId);

            if (vehiculo.CapacidadPasajeros < 1)
            {
                ModelState.AddModelError("", "No hay asientos disponibles.");
                return View(boleto);
            }

            vehiculo.CapacidadPasajeros--;
            _context.Boleto.Add(boleto);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        return View(boleto);
    }
}

