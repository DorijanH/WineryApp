﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WineryApp.Data;
using WineryApp.Data.Entiteti;
using WineryApp.ViewModels.RezultatiAnalize;

namespace WineryApp.Controllers
{
    public class RezultatAnalizeController : Controller
    {
        private readonly WineryAppDbContext _context;
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public RezultatAnalizeController(WineryAppDbContext context, IRepository repository, IMapper mapper)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
        }

        // GET: RezultatAnalize
        public IActionResult Index()
        {
            var allRezultatiAnalize = _repository.GetAllRezultatiAnalize();
            var allZaposlenici = _repository.GetAllZaposleniciBezVlasnika();
            var allSpremnici = _repository.GetAllSpremnici();

            ViewData["Spremnici"] = new SelectList(allSpremnici, nameof(Spremnik.SpremnikId), nameof(Spremnik.ŠifraSpremnika));

            var model = new RezultatAnalizeViewModel
            {
                RezultatiAnalize = allRezultatiAnalize,
                Zaposlenici = allZaposlenici
            };

            return View(model);
        }

        // GET: RezultatAnalize/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezultatAnalize = await _context.RezultatAnalize
                .Include(r => r.Spremnik)
                .Include(r => r.UzorakUzeo)
                .FirstOrDefaultAsync(m => m.RezultatAnalizeId == id);
            if (rezultatAnalize == null)
            {
                return NotFound();
            }

            return View(rezultatAnalize);
        }

        // POST: RezultatAnalize/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DodajRezultatAnalize(RezultatAnalizeIM rezultatAnalizeInput)
        {
            if (ModelState.IsValid)
            {
                var noviRezultatAnalize = _mapper.ToRezultatAnalize(rezultatAnalizeInput);

                _context.Add(noviRezultatAnalize);
                await _context.SaveChangesAsync();

                TempData["Uspješno"] = $"Rezultat analize {noviRezultatAnalize.ŠifraUzorka} je uspješno dodan!";

                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Neuspješno"] = "Rezultat analize nije uspješno dodan!";
                return RedirectToAction("Index");
            }
        }

        // GET: RezultatAnalize/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezultatAnalize = await _context.RezultatAnalize.FindAsync(id);
            if (rezultatAnalize == null)
            {
                return NotFound();
            }
            ViewData["SpremnikId"] = new SelectList(_context.Spremnik, "SpremnikId", "SpremnikId", rezultatAnalize.SpremnikId);
            ViewData["UzorakUzeoId"] = new SelectList(_context.Zaposlenik, "ZaposlenikId", "KorisnickoIme", rezultatAnalize.UzorakUzeoId);
            return View(rezultatAnalize);
        }

        // POST: RezultatAnalize/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RezultatAnalizeId,ŠifraUzorka,DatumUzimanjaUzorka,StatusRezultata,ŠifraPodruma,PhVrijednost,Šećer,RezidualniŠećer,SlobodniSumpor,UkupniSumpor,Kiselina,PostotakAlkohola,UzorakUzeoId,SpremnikId")] RezultatAnalize rezultatAnalize)
        {
            if (id != rezultatAnalize.RezultatAnalizeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rezultatAnalize);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RezultatAnalizeExists(rezultatAnalize.RezultatAnalizeId))
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
            ViewData["SpremnikId"] = new SelectList(_context.Spremnik, "SpremnikId", "SpremnikId", rezultatAnalize.SpremnikId);
            ViewData["UzorakUzeoId"] = new SelectList(_context.Zaposlenik, "ZaposlenikId", "KorisnickoIme", rezultatAnalize.UzorakUzeoId);
            return View(rezultatAnalize);
        }

        // GET: RezultatAnalize/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rezultatAnalize = await _context.RezultatAnalize
                .Include(r => r.Spremnik)
                .Include(r => r.UzorakUzeo)
                .FirstOrDefaultAsync(m => m.RezultatAnalizeId == id);
            if (rezultatAnalize == null)
            {
                return NotFound();
            }

            return View(rezultatAnalize);
        }

        // POST: RezultatAnalize/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rezultatAnalize = await _context.RezultatAnalize.FindAsync(id);
            _context.RezultatAnalize.Remove(rezultatAnalize);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RezultatAnalizeExists(int id)
        {
            return _context.RezultatAnalize.Any(e => e.RezultatAnalizeId == id);
        }
    }
}
