using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlazoFijoSistem.Datos;
using PlazoFijoSistem.Models;

namespace PlazoFijoSistem.Controllers
{
    public class PlazosController : Controller
    {
        private readonly BaseDeDatos _context;

        public PlazosController(BaseDeDatos context)
        {
            _context = context;
        }

        // GET: Plazos
        public async Task<IActionResult> Index()
        {
            return _context.Plazos != null ?
                        View(await _context.Plazos.ToListAsync()) :
                        Problem("Entity set 'BaseDeDatos.Plazos'  is null.");
        }

        // GET: Plazos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Plazos == null)
            {
                return NotFound();
            }

            var plazos = await _context.Plazos
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
            return View();
        }

        // POST: Plazos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Monto,Dias,Bancos")] Plazos plazos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plazos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(plazos);
        }

        // GET: Plazos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Plazos == null)
            {
                return NotFound();
            }

            var plazos = await _context.Plazos.FindAsync(id);
            if (plazos == null)
            {
                return NotFound();
            }
            return View(plazos);
        }

        // POST: Plazos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Monto,Dias,Bancos")] Plazos plazos)
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

        private bool PlazosExists(int id)
        {
            return (_context.Plazos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
