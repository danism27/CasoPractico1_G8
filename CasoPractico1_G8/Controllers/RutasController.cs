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
                .Include(r => r.Horarios)
                .Include(r => r.Paradas)
                .ToListAsync();

            return View(rutas);
        }

        // GET: Rutas/Create
        public IActionResult Create()
        {
            var codigoRuta = "R" + DateTime.Now.ToString("yyyyMMddHHmmss");

            // Cargar las paradas y horarios disponibles
            ViewData["Paradas"] = new MultiSelectList(_context.Parada, "Id", "Nombre");
            ViewData["Horarios"] = new MultiSelectList(_context.Horario, "Id", "HoraSalida");
            ViewData["UsuarioRegistroId"] = new SelectList(_context.Usuario, "Id", "Contraseña");

            var ruta = new Ruta { CodigoRuta = codigoRuta };

            return View(ruta);
        }

        // POST: Rutas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CodigoRuta,Nombre,Descripcion,Estado, FechaRegistro, UsuarioRegistroId")] Ruta ruta, int[] ParadasSeleccionadas, int[] HorariosSeleccionados)
        {
            if (ModelState.IsValid)
            {
                // Generar código de ruta numérico
                var codigoRutaNumerico = "R" + new Random().Next(100000, 999999).ToString();
                ruta.CodigoRuta = codigoRutaNumerico; // Asignación del código numérico a la ruta
                ruta.FechaRegistro = DateTime.Now;
                ruta.UsuarioRegistroId = 1; // Ajustar con el usuario autenticado

                // Añadir la ruta a la base de datos
                _context.Add(ruta);
                await _context.SaveChangesAsync();

                // Guardar las relaciones de paradas seleccionadas
                if (ParadasSeleccionadas?.Length > 0)
                {
                    foreach (var paradaId in ParadasSeleccionadas)
                    {
                        _context.RutaParada.Add(new RutaParada { RutaId = ruta.Id, ParadaId = paradaId });
                    }
                }

                // Guardar las relaciones de horarios seleccionados
                if (HorariosSeleccionados?.Length > 0)
                {
                    foreach (var horarioId in HorariosSeleccionados)
                    {
                        _context.RutaHorario.Add(new RutaHorario { RutaId = ruta.Id, HorarioId = horarioId });
                    }
                }

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Si no es válido, recargar las opciones de paradas y horarios
            ViewData["Paradas"] = new MultiSelectList(_context.Parada, "Id", "Nombre");
            ViewData["Horarios"] = new MultiSelectList(_context.Horario, "Id", "HoraSalida");
            return View(ruta);
        }


        // GET: Rutas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var ruta = await _context.Ruta
                .Include(r => r.Paradas)  // Incluir las paradas asociadas
                .Include(r => r.Horarios) // Incluir los horarios asociados
                .FirstOrDefaultAsync(r => r.Id == id);

            if (ruta == null) return NotFound();

            ViewData["Paradas"] = new MultiSelectList(_context.Parada, "Id", "Nombre", ruta.Paradas.Select(p => p.Parada));
            ViewData["Horarios"] = new MultiSelectList(_context.Horario, "Id", "HoraSalida", ruta.Horarios.Select(h => h));

            return View(ruta);
        }

        // POST: Rutas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CodigoRuta,Nombre,Descripcion,Estado")] Ruta ruta, int[] ParadasSeleccionadas, int[] HorariosSeleccionados)
        {
            if (id != ruta.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ruta);
                    await _context.SaveChangesAsync();

                    // Eliminar las paradas y horarios anteriores
                    var rutaParadasExistentes = _context.RutaParada.Where(rp => rp.RutaId == ruta.Id).ToList();
                    _context.RutaParada.RemoveRange(rutaParadasExistentes);

                    var rutaHorariosExistentes = _context.RutaHorario.Where(rh => rh.RutaId == ruta.Id).ToList();
                    _context.RutaHorario.RemoveRange(rutaHorariosExistentes);

                    // Agregar las nuevas relaciones de paradas y horarios
                    if (ParadasSeleccionadas != null)
                    {
                        foreach (var paradaId in ParadasSeleccionadas)
                        {
                            _context.RutaParada.Add(new RutaParada { RutaId = ruta.Id, ParadaId = paradaId });
                        }
                    }

                    if (HorariosSeleccionados != null)
                    {
                        foreach (var horarioId in HorariosSeleccionados)
                        {
                            _context.RutaHorario.Add(new RutaHorario { RutaId = ruta.Id, HorarioId = horarioId });
                        }
                    }

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

            ViewData["Paradas"] = new MultiSelectList(_context.Parada, "Id", "Nombre", ParadasSeleccionadas);
            ViewData["Horarios"] = new MultiSelectList(_context.Horario, "Id", "HoraSalida", HorariosSeleccionados);
            return View(ruta);
        }

        // GET: Rutas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var ruta = await _context.Ruta
                .Include(r => r.Paradas)  // Incluir las paradas asociadas
                .Include(r => r.Horarios) // Incluir los horarios asociados
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ruta == null) return NotFound();

            return View(ruta);
        }

        // POST: Rutas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ruta = await _context.Ruta.FindAsync(id);
            if (ruta != null)
            {
                // Eliminar las relaciones de paradas y horarios antes de eliminar la ruta
                var rutaParadasExistentes = _context.RutaParada.Where(rp => rp.RutaId == id).ToList();
                _context.RutaParada.RemoveRange(rutaParadasExistentes);

                var rutaHorariosExistentes = _context.RutaHorario.Where(rh => rh.RutaId == id).ToList();
                _context.RutaHorario.RemoveRange(rutaHorariosExistentes);

                _context.Ruta.Remove(ruta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
