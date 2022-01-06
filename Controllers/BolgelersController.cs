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
    public class BolgelersController : Controller
    {
        private readonly ApiPostgresContext _context;

        public BolgelersController(ApiPostgresContext context)
        {
            _context = context;
        }

        // GET: Bolgelers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bolgelers.ToListAsync());
        }

        // GET: Bolgelers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bolgeler = await _context.Bolgelers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bolgeler == null)
            {
                return NotFound();
            }

            return View(bolgeler);
        }

        // GET: Bolgelers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bolgelers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Isim")] Bolgeler bolgeler)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bolgeler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bolgeler);
        }

        // GET: Bolgelers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bolgeler = await _context.Bolgelers.FindAsync(id);
            if (bolgeler == null)
            {
                return NotFound();
            }
            return View(bolgeler);
        }

        // POST: Bolgelers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Isim")] Bolgeler bolgeler)
        {
            if (id != bolgeler.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bolgeler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BolgelerExists(bolgeler.Id))
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
            return View(bolgeler);
        }

        // GET: Bolgelers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bolgeler = await _context.Bolgelers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bolgeler == null)
            {
                return NotFound();
            }

            return View(bolgeler);
        }

        // POST: Bolgelers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bolgeler = await _context.Bolgelers.FindAsync(id);
            _context.Bolgelers.Remove(bolgeler);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BolgelerExists(int id)
        {
            return _context.Bolgelers.Any(e => e.Id == id);
        }
    }
}
