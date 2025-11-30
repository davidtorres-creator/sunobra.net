using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sunobra.Model;

namespace sunobra.Controllers
{
    public class ContratacionesController : Controller
    {
        private readonly SunobraDbContext _context;

        public ContratacionesController(SunobraDbContext context)
        {
            _context = context;
        }

        // GET: Contrataciones
        public async Task<IActionResult> Index()
        {
            var sunobraDbContext = _context.Contrataciones.Include(c => c.Cliente).Include(c => c.Obrero).Include(c => c.Proyecto);
            return View(await sunobraDbContext.ToListAsync());
        }

        // GET: Contrataciones/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contratacione = await _context.Contrataciones
                .Include(c => c.Cliente)
                .Include(c => c.Obrero)
                .Include(c => c.Proyecto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contratacione == null)
            {
                return NotFound();
            }

            return View(contratacione);
        }

        // GET: Contrataciones/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Usuarios, "Id", "Id");
            ViewData["ObreroId"] = new SelectList(_context.Usuarios, "Id", "Id");
            ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "Id", "Id");
            return View();
        }

        // POST: Contrataciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClienteId,ObreroId,ProyectoId,FechaContratacion,FechaInicio,FechaFin,TarifaTotal,Estado,Descripcion,CalificacionCliente,CalificacionObrero,ComentariosCliente,ComentariosObrero")] Contratacione contratacione)
        {
            if (ModelState.IsValid)
            {

                contratacione.FechaContratacion = DateTime.UtcNow;

                _context.Add(contratacione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Usuarios, "Id", "Id", contratacione.ClienteId);
            ViewData["ObreroId"] = new SelectList(_context.Usuarios, "Id", "Id", contratacione.ObreroId);
            ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "Id", "Id", contratacione.ProyectoId);
            return View(contratacione);
        }

        // GET: Contrataciones/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contratacione = await _context.Contrataciones.FindAsync(id);
            if (contratacione == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Usuarios, "Id", "Id", contratacione.ClienteId);
            ViewData["ObreroId"] = new SelectList(_context.Usuarios, "Id", "Id", contratacione.ObreroId);
            ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "Id", "Id", contratacione.ProyectoId);
            return View(contratacione);
        }

        // POST: Contrataciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,ClienteId,ObreroId,ProyectoId,FechaContratacion,FechaInicio,FechaFin,TarifaTotal,Estado,Descripcion,CalificacionCliente,CalificacionObrero,ComentariosCliente,ComentariosObrero")] Contratacione contratacione)
        {
            if (id != contratacione.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contratacione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContratacioneExists(contratacione.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.Usuarios, "Id", "Id", contratacione.ClienteId);
            ViewData["ObreroId"] = new SelectList(_context.Usuarios, "Id", "Id", contratacione.ObreroId);
            ViewData["ProyectoId"] = new SelectList(_context.Proyectos, "Id", "Id", contratacione.ProyectoId);
            return View(contratacione);
        }

        // GET: Contrataciones/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contratacione = await _context.Contrataciones
                .Include(c => c.Cliente)
                .Include(c => c.Obrero)
                .Include(c => c.Proyecto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contratacione == null)
            {
                return NotFound();
            }

            return View(contratacione);
        }

        // POST: Contrataciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var contratacione = await _context.Contrataciones.FindAsync(id);
            if (contratacione != null)
            {
                _context.Contrataciones.Remove(contratacione);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContratacioneExists(long id)
        {
            return _context.Contrataciones.Any(e => e.Id == id);
        }
    }
}
