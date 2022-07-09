using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlazoFijoSistem.Datos;
using PlazoFijoSistem.Models;

namespace PlazoFijoSistem.Controllers
{
    [Authorize(Roles = "ADMIN, USER")]
    public class PlazosController : ControladorBase
    {
        private readonly BaseDeDatos _context;

        public PlazosController(BaseDeDatos context)
        {
            _context = context;
        }

        // GET: Plazos
        public async Task<IActionResult> Index()
        { 
            // aca se obtiene la claim principal, del idUsuario
            //var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            //aca dice que el id del usuario --> se recupera del login

            int idUsuario = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            string rol = User.FindFirstValue(ClaimTypes.Role);

            if (rol.Equals("ADMIN")) 
            {
                var baseDeDatosDeUsuario = _context
                .Plazos
                .Include(p => p.Banco)
                .Include(p => p.Usuario);
                return View(await baseDeDatosDeUsuario.ToListAsync());
            }
            else
            {
                var baseDeDatosDeUsuario = _context
                .Plazos
                .Where(o => o.UsuarioId == idUsuario)
                .Include(p => p.Banco)
                .Include(p => p.Usuario);
                return View(await baseDeDatosDeUsuario.ToListAsync());

            }
            

            
        }

        // GET: Plazos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Plazos == null)
            {
                return NotFound();
            }

            var plazos = await _context.Plazos
                .Include(p => p.Banco)
                .Include(p => p.Usuario)
                
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plazos == null)
            {
                return NotFound();
            }

            return View(plazos);
        }

        // GET: Plazos/Create
        public IActionResult Create()
        {
            ViewData["BancoId"] = new SelectList(_context.Bancos, "id", "RazonSocial");
            if (this.EsAdmin)
            {
                ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Email");
            }
            else 
            {
                ViewData["UsuarioId"] = new SelectList(_context.Usuarios.Where(o=>o.Id == IdUsuario).ToList(), "Id", "Email");
            }
            
            return View();
        }

        // POST: Plazos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Monto,Dias,BancoId,UsuarioId")] Plazos plazos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plazos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BancoId"] = new SelectList(_context.Bancos, "id", "id", plazos.BancoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", plazos.UsuarioId);
            return View(plazos);
        }

        // GET: Plazos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Plazos == null)
            {
                return  NotFound();
            }

            var plazos = await _context.Plazos.FindAsync(id);
            if (plazos == null)
            {
                return NotFound();
            }
            ViewData["BancoId"] = new SelectList(_context.Bancos, "id", "id", plazos.BancoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", plazos.UsuarioId);
            return View(plazos);
        }

        // POST: Plazos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Monto,Dias,BancoId,UsuarioId")] Plazos plazos)
        {
            if (id != plazos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plazos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlazosExists(plazos.Id))
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
            ViewData["BancoId"] = new SelectList(_context.Bancos, "id", "id", plazos.BancoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", plazos.UsuarioId);
            return View(plazos);
        }

        // GET: Plazos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Plazos == null)
            {
                return NotFound();
            }

            var plazos = await _context.Plazos
                .Include(p => p.Banco)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plazos == null)
            {
                return NotFound();
            }

            return View(plazos);
        }

        // POST: Plazos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Plazos == null)
            {
                return Problem("Entity set 'BaseDeDatos.Plazos'  is null.");
            }
            var plazos = await _context.Plazos.FindAsync(id);
            if (plazos != null)
            {
                _context.Plazos.Remove(plazos);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: Plazos/Calcular
        public IActionResult Calcular()
            {
            ViewData["BancoId"] = new SelectList(_context.Bancos, "id", "RazonSocial");
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Calcular([Bind("Id,Monto,Dias,BancoId,UsuarioId")] Plazos plazos)
        {

            

            if (ModelState.IsValid)
            {
                var banco = await _context.Bancos
                .FirstOrDefaultAsync(m => m.id == plazos.BancoId);

                var monto = plazos.Monto;
                var dias = plazos.Dias;
                var tasa = banco.Porcentaje;
                var resultadoAnual =  365/tasa;
                var resultadofinal = (monto/(resultadoAnual * dias))+monto ;
                ViewBag.Resultado = resultadofinal;
            }

            
            return View(plazos);
        }


        private bool PlazosExists(int id)
        {
          return (_context.Plazos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
