using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CasoPractico1_G8.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CasoPractico1_G8.Controllers
{
    public class RutasController : Controller
    {
        private readonly CasoPractico1_G8Context _context;

        public RutasController(CasoPractico1_G8Context context)
        {
            _context = context;
        }

        // GET: Rutas
        public async Task<IActionResult> Index()
        {
            var rutas = await _context.Ruta
                .ToListAsync();

            return View(rutas);
        }

        // GET: Rutas/Create
        public IActionResult Create()
        {
            // Cambiado el SelectList para mostrar "Id" y "NombreCompleto"
            ViewData["UsuarioRegistroId"] = new SelectList(_context.Usuario, "Id", "NombreCompleto");
            return View();
        }

        // POST: Rutas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Descripcion,Estado,ParadasTexto,HorariosTexto,UsuarioRegistroId")] Ruta ruta)
        {
            if (ModelState.IsValid)
            {
                // Establecemos la fecha de registro
                ruta.FechaRegistro = DateTime.Now;

                // Guardamos la ruta en la base de datos
                _context.Add(ruta);
                await _context.SaveChangesAsync();

                // Redirigimos al listado de rutas
                return RedirectToAction(nameof(Index));
            }

            // Si la validación falla, recargamos la lista de usuarios
            ViewData["UsuarioRegistroId"] = new SelectList(_context.Usuario, "Id", "NombreCompleto", ruta.UsuarioRegistroId);
            return View(ruta);
        }

        // GET: Rutas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var ruta = await _context.Ruta.FirstOrDefaultAsync(r => r.Id == id);

            if (ruta == null) return NotFound();

            ViewData["Estado"] = new SelectList(new[]
            {
                new { Value = "true", Text = "Activa" },
                new { Value = "false", Text = "Inactiva" }
            }, "Value", "Text", ruta.Estado.ToString());

            ViewData["UsuarioRegistroId"] = new SelectList(_context.Usuario, "Id", "NombreCompleto", ruta.UsuarioRegistroId);

            return View(ruta);
        }

        // POST: Rutas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,Estado,ParadasTexto,HorariosTexto,UsuarioRegistroId")] Ruta ruta)
        {
            if (id != ruta.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ruta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Ruta.Any(e => e.Id == ruta.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["Estado"] = new SelectList(new[]
            {
                new { Value = "true", Text = "Activa" },
                new { Value = "false", Text = "Inactiva" }
            }, "Value", "Text", ruta.Estado.ToString());

            ViewData["UsuarioRegistroId"] = new SelectList(_context.Usuario, "Id", "NombreCompleto", ruta.UsuarioRegistroId);

            return View(ruta);
        }

        // GET: Rutas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var ruta = await _context.Ruta.FirstOrDefaultAsync(m => m.Id == id);

            if (ruta == null) return NotFound();

            return View(ruta);
        }

        // POST: Rutas/Delete/5
        [HttpPost, ActionName("Delete")] // Mantenemos el alias "Delete"
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ruta = await _context.Ruta.FindAsync(id);
            if (ruta != null)
            {
                _context.Ruta.Remove(ruta);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
