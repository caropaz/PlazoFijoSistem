using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlazoFijoSistem.Datos;
using PlazoFijoSistem.Models;

namespace PlazoFijoSistem.Controllers
{
    public class BancosController : Controller
    {
        private readonly BaseDeDatos _context;

        public BancosController(BaseDeDatos context)
        {
            _context = context;
        }

        // GET: Bancos
        public async Task<IActionResult> Index()
        {
              return _context.Bancos != null ? 
                          View(await _context.Bancos.ToListAsync()) :
                          Problem("Entity set 'BaseDeDatos.Bancos'  is null.");
        }

        // GET: Bancos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bancos == null)
            {
                return NotFound();
            }

            var bancos = await _context.Bancos
                .FirstOrDefaultAsync(m => m.id == id);
            if (bancos == null)
            {
                return NotFound();
            }

            return View(bancos);
        }

        // GET: Bancos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bancos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,RazonSocial,Porcentaje")] Bancos bancos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bancos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bancos);
        }

        // GET: Bancos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bancos == null)
            {
                return NotFound();
            }

            var bancos = await _context.Bancos.FindAsync(id);
            if (bancos == null)
            {
                return NotFound();
            }
            return View(bancos);
        }

        // POST: Bancos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,RazonSocial,Porcentaje")] Bancos bancos)
        {
            if (id != bancos.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bancos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BancosExists(bancos.id))
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
            return View(bancos);
        }

        // GET: Bancos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bancos == null)
            {
                return NotFound();
            }

            var bancos = await _context.Bancos
                .FirstOrDefaultAsync(m => m.id == id);
            if (bancos == null)
            {
                return NotFound();
            }

            return View(bancos);
        }

        // POST: Bancos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bancos == null)
            {
                return Problem("Entity set 'BaseDeDatos.Bancos'  is null.");
            }
            var bancos = await _context.Bancos.FindAsync(id);
            if (bancos != null)
            {
                _context.Bancos.Remove(bancos);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BancosExists(int id)
        {
          return (_context.Bancos?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
