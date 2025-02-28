using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CasoPractico1_G8.Models;
using Microsoft.AspNetCore.Identity;

namespace CasoPractico1_G8.Controllers
{
    public class VehiculosController : Controller
    {
        private readonly CasoPractico1_G8Context _context;
        private readonly UserManager<Usuario> _userManager; 

        public VehiculosController(CasoPractico1_G8Context context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager; // Initialize UserManager
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Vehiculo.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vehiculo vehiculo)
        {
            if (ModelState.IsValid)
            {
                vehiculo.FechaRegistro = DateTime.Now;
                var user = await _userManager.GetUserAsync(User); 
                if (user == null) return Unauthorized(); 
                vehiculo.UsuarioRegistro = user; 
                _context.Add(vehiculo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vehiculo);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var vehiculo = await _context.Vehiculo.FindAsync(id);
            if (vehiculo == null) return NotFound();
            return View(vehiculo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Vehiculo vehiculo)
        {
            if (id != vehiculo.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(vehiculo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vehiculo);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var vehiculo = await _context.Vehiculo.FindAsync(id);
            if (vehiculo == null) return NotFound();
            return View(vehiculo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehiculo = await _context.Vehiculo.FindAsync(id);
            _context.Vehiculo.Remove(vehiculo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
