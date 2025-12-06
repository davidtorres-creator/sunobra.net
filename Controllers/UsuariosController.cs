using Microsoft.AspNetCore.Authorization;
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
    [Authorize]  // ⬅⬅⬅ Protección con Identity
    public class UsuariosController : Controller
    {
        private readonly SunobraDbContext _context;

        public UsuariosController(SunobraDbContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usuarios.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            ViewBag.UserTypes = new List<SelectListItem>
            {
                new SelectListItem { Text = "Cliente", Value = "Cliente" },
                new SelectListItem { Text = "Obrero", Value = "Obrero" }
            };

            ViewBag.PreferenciasContacto = new List<SelectListItem>
            {
                new SelectListItem { Text = "Teléfono", Value = "Teléfono" },
                new SelectListItem { Text = "WhatsApp", Value = "WhatsApp" },
                new SelectListItem { Text = "Email", Value = "Email" }
            };

            ViewBag.Especialidades = new List<SelectListItem>
            {
                new SelectListItem { Text = "Albañilería", Value = "Albañilería" },
                new SelectListItem { Text = "Pintura", Value = "Pintura" },
                new SelectListItem { Text = "Electricidad", Value = "Electricidad" },
                new SelectListItem { Text = "Plomería", Value = "Plomería" },
                new SelectListItem { Text = "Carpintería", Value = "Carpintería" },
                new SelectListItem { Text = "Soldadura", Value = "Soldadura" }
            };

            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Apellido,Email,Password,UserType,Telefono,Direccion,PreferenciasContacto,Especialidades,Experiencia,TarifaHora,Certificaciones,Descripcion,FechaRegistro,Activo")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nombre,Apellido,Email,Password,UserType,Telefono,Direccion,PreferenciasContacto,Especialidades,Experiencia,TarifaHora,Certificaciones,Descripcion,FechaRegistro,Activo")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
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
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(long id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
