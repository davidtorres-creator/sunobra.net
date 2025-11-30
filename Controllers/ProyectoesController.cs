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
    public class ProyectoesController : Controller
    {
        private readonly SunobraDbContext _context;

        public ProyectoesController(SunobraDbContext context)
        {
            _context = context;
        }

        // GET: Proyectoes
        public async Task<IActionResult> Index()
        {
            var sunobraDbContext = _context.Proyectos.Include(p => p.Cliente).Include(p => p.Obrero);
            return View(await sunobraDbContext.ToListAsync());
        }

        // GET: Proyectoes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyectos
                .Include(p => p.Cliente)
                .Include(p => p.Obrero)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proyecto == null)
            {
                return NotFound();
            }

            return View(proyecto);
        }

        // GET: Proyectoes/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Usuarios, "Id", "Id");
            ViewData["ObreroId"] = new SelectList(_context.Usuarios, "Id", "Id");
            return View();
        }

        // POST: Proyectoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Descripcion,Categoria,ImagenUrl,Ubicacion,ClienteId,ObreroId,Estado,FechaCreacion,FechaInicio,FechaFin,Presupuesto")] Proyecto proyecto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proyecto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Usuarios, "Id", "Id", proyecto.ClienteId);
            ViewData["ObreroId"] = new SelectList(_context.Usuarios, "Id", "Id", proyecto.ObreroId);
            return View(proyecto);
        }

        // GET: Proyectoes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyectos.FindAsync(id);
            if (proyecto == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Usuarios, "Id", "Id", proyecto.ClienteId);
            ViewData["ObreroId"] = new SelectList(_context.Usuarios, "Id", "Id", proyecto.ObreroId);
            return View(proyecto);
        }

        // POST: Proyectoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Titulo,Descripcion,Categoria,ImagenUrl,Ubicacion,ClienteId,ObreroId,Estado,FechaCreacion,FechaInicio,FechaFin,Presupuesto")] Proyecto proyecto)
        {
            if (id != proyecto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proyecto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProyectoExists(proyecto.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.Usuarios, "Id", "Id", proyecto.ClienteId);
            ViewData["ObreroId"] = new SelectList(_context.Usuarios, "Id", "Id", proyecto.ObreroId);
            return View(proyecto);
        }

        // GET: Proyectoes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proyecto = await _context.Proyectos
                .Include(p => p.Cliente)
                .Include(p => p.Obrero)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proyecto == null)
            {
                return NotFound();
            }

            return View(proyecto);
        }

        // POST: Proyectoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var proyecto = await _context.Proyectos.FindAsync(id);
            if (proyecto != null)
            {
                _context.Proyectos.Remove(proyecto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProyectoExists(long id)
        {
            return _context.Proyectos.Any(e => e.Id == id);
        }
    }
}
