using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WineryApp.Data;
using WineryApp.Data.Entiteti;

namespace WineryApp.Controllers
{
    public class AditiviController : Controller
    {
        private readonly WineryAppDbContext _context;

        public AditiviController(WineryAppDbContext context)
        {
            _context = context;
        }

        // GET: Aditivi
        public async Task<IActionResult> Index()
        {
            var wineryAppDbContext = _context.Aditiv.Include(a => a.VrstaAditiva);
            return View(await wineryAppDbContext.ToListAsync());
        }

        // GET: Aditivi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aditiv = await _context.Aditiv
                .Include(a => a.VrstaAditiva)
                .FirstOrDefaultAsync(m => m.AditivId == id);
            if (aditiv == null)
            {
                return NotFound();
            }

            return View(aditiv);
        }

        // POST: Aditivi/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AditivId,ImeAditiva,Koncentracija,Količina,Instrukcije,VrstaAditivaId")] Aditiv aditiv)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aditiv);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VrstaAditivaId"] = new SelectList(_context.VrstaAditiva, "VrstaAditivaId", "VrstaAditivaId", aditiv.VrstaAditivaId);
            return View(aditiv);
        }

        // GET: Aditivi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aditiv = await _context.Aditiv.FindAsync(id);
            if (aditiv == null)
            {
                return NotFound();
            }
            ViewData["VrstaAditivaId"] = new SelectList(_context.VrstaAditiva, "VrstaAditivaId", "VrstaAditivaId", aditiv.VrstaAditivaId);
            return View(aditiv);
        }

        // POST: Aditivi/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AditivId,ImeAditiva,Koncentracija,Količina,Instrukcije,VrstaAditivaId")] Aditiv aditiv)
        {
            if (id != aditiv.AditivId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aditiv);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AditivExists(aditiv.AditivId))
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
            ViewData["VrstaAditivaId"] = new SelectList(_context.VrstaAditiva, "VrstaAditivaId", "VrstaAditivaId", aditiv.VrstaAditivaId);
            return View(aditiv);
        }

        // POST: Aditivi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aditiv = await _context.Aditiv.FindAsync(id);
            _context.Aditiv.Remove(aditiv);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AditivExists(int id)
        {
            return _context.Aditiv.Any(e => e.AditivId == id);
        }
    }
}
