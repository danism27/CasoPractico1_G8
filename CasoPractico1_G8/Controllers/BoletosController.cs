using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CasoPractico1_G8.Models;

namespace CasoPractico1_G8.Controllers
{
    public class BoletosController : Controller
    {
        private readonly CasoPractico1_G8Context _context;

        public BoletosController(CasoPractico1_G8Context context)
        {
            _context = context;
        }

        // GET: Boletos
        public async Task<IActionResult> Index()
        {
            var casoPractico1_G8Context = _context.Boleto.Include(b => b.Ruta).Include(b => b.Usuario);
            return View(await casoPractico1_G8Context.ToListAsync());
        }

        // GET: Boletoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boleto = await _context.Boleto
                .Include(b => b.Ruta)
                .Include(b => b.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (boleto == null)
            {
                return NotFound();
            }

            return View(boleto);
        }

        // GET: Boletoes/Create
        public IActionResult Create()
        {
            ViewData["RutaId"] = new SelectList(_context.Ruta, "Id", "Nombre");
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Contraseña");
            return View();
        }

        // POST: Boletoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UsuarioId,RutaId,FechaCompra,Activo")] Boleto boleto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(boleto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RutaId"] = new SelectList(_context.Ruta, "Id", "Nombre", boleto.RutaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Contraseña", boleto.UsuarioId);
            return View(boleto);
        }

        // GET: Boletoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boleto = await _context.Boleto.FindAsync(id);
            if (boleto == null)
            {
                return NotFound();
            }
            ViewData["RutaId"] = new SelectList(_context.Ruta, "Id", "Nombre", boleto.RutaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Contraseña", boleto.UsuarioId);
            return View(boleto);
        }

        // POST: Boletoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UsuarioId,RutaId,FechaCompra,Activo")] Boleto boleto)
        {
            if (id != boleto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(boleto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoletoExists(boleto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RutaId"] = new SelectList(_context.Ruta, "Id", "Nombre", boleto.RutaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Contraseña", boleto.UsuarioId);
            return View(boleto);
        }

        // GET: Boletoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boleto = await _context.Boleto
                .Include(b => b.Ruta)
                .Include(b => b.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (boleto == null)
            {
                return NotFound();
            }

            return View(boleto);
        }

        // POST: Boletoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var boleto = await _context.Boleto.FindAsync(id);
            if (boleto != null)
            {
                _context.Boleto.Remove(boleto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BoletoExists(int id)
        {
            return _context.Boleto.Any(e => e.Id == id);
        }
    }
}
