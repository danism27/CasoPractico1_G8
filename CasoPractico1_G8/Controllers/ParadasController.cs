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
    public class ParadasController : Controller
    {
        private readonly CasoPractico1_G8Context _context;

        public ParadasController(CasoPractico1_G8Context context)
        {
            _context = context;
        }

        // GET: Paradas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Parada.ToListAsync());
        }

        // GET: Paradas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parada = await _context.Parada
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parada == null)
            {
                return NotFound();
            }

            return View(parada);
        }

        // GET: Paradas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Paradas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion")] Parada parada)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parada);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parada);
        }

        // GET: Paradas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parada = await _context.Parada.FindAsync(id);
            if (parada == null)
            {
                return NotFound();
            }
            return View(parada);
        }

        // POST: Paradas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion")] Parada parada)
        {
            if (id != parada.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parada);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParadaExists(parada.Id))
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
            return View(parada);
        }

        // GET: Paradas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parada = await _context.Parada
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parada == null)
            {
                return NotFound();
            }

            return View(parada);
        }

        // POST: Paradas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parada = await _context.Parada.FindAsync(id);
            if (parada != null)
            {
                _context.Parada.Remove(parada);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParadaExists(int id)
        {
            return _context.Parada.Any(e => e.Id == id);
        }
    }
}
