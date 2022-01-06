using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApiDeneme14.ModelsGenerated;

namespace WebApiDeneme14.Controllers
{
    public class SehirlersController : Controller
    {
        private readonly ApiPostgresContext _context;

        public SehirlersController(ApiPostgresContext context)
        {
            _context = context;
        }

        // GET: Sehirlers
        public async Task<IActionResult> Index()
        {
            var apiPostgresContext = _context.Sehirlers.Include(s => s.Bolge);
            return View(await apiPostgresContext.ToListAsync());
        }

        // GET: Sehirlers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sehirler = await _context.Sehirlers
                .Include(s => s.Bolge)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sehirler == null)
            {
                return NotFound();
            }

            return View(sehirler);
        }

        // GET: Sehirlers/Create
        public IActionResult Create()
        {
            ViewData["Bolgeid"] = new SelectList(_context.Bolgelers, "Id", "Id");
            return View();
        }

        // POST: Sehirlers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Isim,Bolgeid")] Sehirler sehirler)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sehirler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Bolgeid"] = new SelectList(_context.Bolgelers, "Id", "Id", sehirler.Bolgeid);
            return View(sehirler);
        }

        // GET: Sehirlers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sehirler = await _context.Sehirlers.FindAsync(id);
            if (sehirler == null)
            {
                return NotFound();
            }
            ViewData["Bolgeid"] = new SelectList(_context.Bolgelers, "Id", "Id", sehirler.Bolgeid);
            return View(sehirler);
        }

        // POST: Sehirlers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Isim,Bolgeid")] Sehirler sehirler)
        {
            if (id != sehirler.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sehirler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SehirlerExists(sehirler.Id))
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
            ViewData["Bolgeid"] = new SelectList(_context.Bolgelers, "Id", "Id", sehirler.Bolgeid);
            return View(sehirler);
        }

        // GET: Sehirlers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sehirler = await _context.Sehirlers
                .Include(s => s.Bolge)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sehirler == null)
            {
                return NotFound();
            }

            return View(sehirler);
        }

        // POST: Sehirlers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sehirler = await _context.Sehirlers.FindAsync(id);
            _context.Sehirlers.Remove(sehirler);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SehirlerExists(int id)
        {
            return _context.Sehirlers.Any(e => e.Id == id);
        }
    }
}
