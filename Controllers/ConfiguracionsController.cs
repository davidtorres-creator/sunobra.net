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
    public class ConfiguracionsController : Controller
    {
        private readonly SunobraDbContext _context;

        public ConfiguracionsController(SunobraDbContext context)
        {
            _context = context;
        }

        // GET: Configuracions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Configuracions.ToListAsync());
        }

        // GET: Configuracions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configuracion = await _context.Configuracions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (configuracion == null)
            {
                return NotFound();
            }

            return View(configuracion);
        }

        // GET: Configuracions/Create
        public IActionResult Create()
        {
            ViewBag.Categorias = new List<string>
            {
                "sistema",
                "email",
                "seguridad",
                "ui",
                "pagos"
            };

            return View();
        }

        // POST: Configuracions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Clave,Valor,Descripcion,FechaActualizacion")] Configuracion configuracion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(configuracion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(configuracion);
        }

        // GET: Configuracions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configuracion = await _context.Configuracions.FindAsync(id);
            if (configuracion == null)
            {
                return NotFound();
            }
            return View(configuracion);
        }

        // POST: Configuracions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Clave,Valor,Descripcion,FechaActualizacion")] Configuracion configuracion)
        {
            if (id != configuracion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(configuracion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConfiguracionExists(configuracion.Id))
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
            return View(configuracion);
        }

        // GET: Configuracions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configuracion = await _context.Configuracions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (configuracion == null)
            {
                return NotFound();
            }

            return View(configuracion);
        }

        // POST: Configuracions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var configuracion = await _context.Configuracions.FindAsync(id);
            if (configuracion != null)
            {
                _context.Configuracions.Remove(configuracion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConfiguracionExists(int id)
        {
            return _context.Configuracions.Any(e => e.Id == id);
        }
    }
}
