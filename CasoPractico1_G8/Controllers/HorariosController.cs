using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CasoPractico1_G8.Models;

namespace CasoPractico1_G8.Controllers
{
    public class HorariosController : Controller
    {
        private readonly CasoPractico1_G8Context _context;

        public HorariosController(CasoPractico1_G8Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Horario.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var horario = await _context.Horario.FirstOrDefaultAsync(m => m.Id == id);
            if (horario == null) return NotFound();

            return View(horario);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HoraSalida,HoraLlegada")] Horario horario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(horario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(horario);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var horario = await _context.Horario.FindAsync(id);
            if (horario == null) return NotFound();

            return View(horario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HoraSalida,HoraLlegada")] Horario horario)
        {
            if (id != horario.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(horario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Horario.Any(e => e.Id == horario.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(horario);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var horario = await _context.Horario.FirstOrDefaultAsync(m => m.Id == id);
            if (horario == null) return NotFound();

            return View(horario);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var horario = await _context.Horario.FindAsync(id);
            if (horario != null) _context.Horario.Remove(horario);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
