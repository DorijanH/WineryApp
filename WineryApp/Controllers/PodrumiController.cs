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
    public class PodrumiController : Controller
    {
        private readonly WineryAppDbContext _context;

        public PodrumiController(WineryAppDbContext context)
        {
            _context = context;
        }

        // GET: Podrumi
        public async Task<IActionResult> Index()
        {
            var wineryAppDbContext = _context.Podrum.Include(p => p.RezultatAnalize).Include(p => p.SortaVina);
            return View(await wineryAppDbContext.ToListAsync());
        }

        // GET: Podrumi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var podrum = await _context.Podrum
                .Include(p => p.RezultatAnalize)
                .Include(p => p.SortaVina)
                .FirstOrDefaultAsync(m => m.PodrumId == id);
            if (podrum == null)
            {
                return NotFound();
            }

            return View(podrum);
        }

        // GET: Podrumi/Create
        public IActionResult Create()
        {
            ViewData["RezultatAnalizeId"] = new SelectList(_context.RezultatAnalize, "RezultatAnalizeId", "RezultatAnalizeId");
            ViewData["SortaVinaId"] = new SelectList(_context.SortaVina, "SortaVinaId", "SortaVinaId");
            return View();
        }

        // POST: Podrumi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PodrumId,ŠifraPodruma,Popunjenost,GodinaBerbe,FazaIzrade,Lokacija,SortaVinaId,RezultatAnalizeId")] Podrum podrum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(podrum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RezultatAnalizeId"] = new SelectList(_context.RezultatAnalize, "RezultatAnalizeId", "RezultatAnalizeId", podrum.RezultatAnalizeId);
            ViewData["SortaVinaId"] = new SelectList(_context.SortaVina, "SortaVinaId", "SortaVinaId", podrum.SortaVinaId);
            return View(podrum);
        }

        // GET: Podrumi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var podrum = await _context.Podrum.FindAsync(id);
            if (podrum == null)
            {
                return NotFound();
            }
            ViewData["RezultatAnalizeId"] = new SelectList(_context.RezultatAnalize, "RezultatAnalizeId", "RezultatAnalizeId", podrum.RezultatAnalizeId);
            ViewData["SortaVinaId"] = new SelectList(_context.SortaVina, "SortaVinaId", "SortaVinaId", podrum.SortaVinaId);
            return View(podrum);
        }

        // POST: Podrumi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PodrumId,ŠifraPodruma,Popunjenost,GodinaBerbe,FazaIzrade,Lokacija,SortaVinaId,RezultatAnalizeId")] Podrum podrum)
        {
            if (id != podrum.PodrumId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(podrum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PodrumExists(podrum.PodrumId))
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
            ViewData["RezultatAnalizeId"] = new SelectList(_context.RezultatAnalize, "RezultatAnalizeId", "RezultatAnalizeId", podrum.RezultatAnalizeId);
            ViewData["SortaVinaId"] = new SelectList(_context.SortaVina, "SortaVinaId", "SortaVinaId", podrum.SortaVinaId);
            return View(podrum);
        }

        // GET: Podrumi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var podrum = await _context.Podrum
                .Include(p => p.RezultatAnalize)
                .Include(p => p.SortaVina)
                .FirstOrDefaultAsync(m => m.PodrumId == id);
            if (podrum == null)
            {
                return NotFound();
            }

            return View(podrum);
        }

        // POST: Podrumi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var podrum = await _context.Podrum.FindAsync(id);
            _context.Podrum.Remove(podrum);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PodrumExists(int id)
        {
            return _context.Podrum.Any(e => e.PodrumId == id);
        }
    }
}
