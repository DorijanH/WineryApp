using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WineryApp.Data;
using WineryApp.Data.Entiteti;
using WineryApp.ViewModels.Spremnici;

namespace WineryApp.Controllers
{
    public class SpremniciController : Controller
    {
        private readonly WineryAppDbContext _context;
        private readonly IRepository _repository;

        public SpremniciController(WineryAppDbContext context, IRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        // GET: Spremnici
        public IActionResult Index()
        {
            var allSpremnici = _repository.GetAllSpremnici();
            var allZaposlenici = _repository.GetAllZaposlenici()
                .Where(z => z.UlogaId == (int) Uloge.Zaposlenik)
                .OrderBy(z => z.Prezime)
                .ToList();

            ViewData["VrsteSpremnika"] = new SelectList(_context.VrstaSpremnika, nameof(VrstaSpremnika.VrstaSpremnikaId), nameof(VrstaSpremnika.NazivVrste));
            ViewData["Berbe"] = new SelectList(_context.Berba, nameof(Berba.BerbaId), nameof(Berba.GodinaBerbe));
            ViewData["Podrumi"] = new SelectList(_context.Podrum, nameof(Podrum.PodrumId), nameof(Podrum.ŠifraPodruma));
            ViewData["Sorte"] = new SelectList(_context.SortaVina, nameof(SortaVina.SortaVinaId), nameof(SortaVina.NazivSorte));

            var model = new SpremniciViewModel
            {
                Spremnici = allSpremnici,
                Zaposlenici = allZaposlenici
            };

            return View(model);
        }

        // GET: Spremnici/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spremnik = await _context.Spremnik
                .Include(s => s.Berba)
                .Include(s => s.Podrum)
                .Include(s => s.Punilac)
                .Include(s => s.SortaVina)
                .Include(s => s.VrstaSpremnika)
                .FirstOrDefaultAsync(m => m.SpremnikId == id);

            ViewData["VrsteSpremnika"] = new SelectList(_context.VrstaSpremnika, nameof(VrstaSpremnika.VrstaSpremnikaId), nameof(VrstaSpremnika.NazivVrste));
            ViewData["Berbe"] = new SelectList(_context.Berba, nameof(Berba.BerbaId), nameof(Berba.GodinaBerbe));

            if (spremnik == null)
            {
                return NotFound();
            }

            return View(spremnik);
        }

        // GET: Spremnici/Create
        public IActionResult Create()
        {
            ViewData["BerbaId"] = new SelectList(_context.Berba, "BerbaId", "BerbaId");
            ViewData["PodrumId"] = new SelectList(_context.Podrum, "PodrumId", "PodrumId");
            ViewData["PunilacId"] = new SelectList(_context.Zaposlenik, "ZaposlenikId", "KorisnickoIme");
            ViewData["SortaVinaId"] = new SelectList(_context.SortaVina, "SortaVinaId", "SortaVinaId");
            ViewData["VrstaSpremnikaId"] = new SelectList(_context.VrstaSpremnika, "VrstaSpremnikaId", "VrstaSpremnikaId");
            return View();
        }

        // POST: Spremnici/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DodajSpremnik(SpremnikIM spremnikInput)
        {
            if (ModelState.IsValid)
            {
                _context.Add(spremnikInput);
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BerbaId"] = new SelectList(_context.Berba, "BerbaId", "BerbaId", spremnikInput.BerbaId);
            ViewData["PodrumId"] = new SelectList(_context.Podrum, "PodrumId", "PodrumId", spremnikInput.PodrumId);
            ViewData["PunilacId"] = new SelectList(_context.Zaposlenik, "ZaposlenikId", "KorisnickoIme", spremnikInput.PunilacId);
            ViewData["SortaVinaId"] = new SelectList(_context.SortaVina, "SortaVinaId", "SortaVinaId", spremnikInput.SortaVinaId);
            ViewData["VrstaSpremnikaId"] = new SelectList(_context.VrstaSpremnika, "VrstaSpremnikaId", "VrstaSpremnikaId", spremnikInput.VrstaSpremnikaId);
            return View(spremnikInput);
        }

        // GET: Spremnici/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spremnik = await _context.Spremnik.FindAsync(id);
            if (spremnik == null)
            {
                return NotFound();
            }
            ViewData["BerbaId"] = new SelectList(_context.Berba, "BerbaId", "BerbaId", spremnik.BerbaId);
            ViewData["PodrumId"] = new SelectList(_context.Podrum, "PodrumId", "PodrumId", spremnik.PodrumId);
            ViewData["PunilacId"] = new SelectList(_context.Zaposlenik, "ZaposlenikId", "KorisnickoIme", spremnik.PunilacId);
            ViewData["SortaVinaId"] = new SelectList(_context.SortaVina, "SortaVinaId", "SortaVinaId", spremnik.SortaVinaId);
            ViewData["VrstaSpremnikaId"] = new SelectList(_context.VrstaSpremnika, "VrstaSpremnikaId", "VrstaSpremnikaId", spremnik.VrstaSpremnikaId);
            return View(spremnik);
        }

        // POST: Spremnici/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SpremnikId,ŠifraSpremnika,Kapacitet,Napunjenost,FazaIzrade,VrstaSpremnikaId,BerbaId,PunilacId,PodrumId,SortaVinaId")] Spremnik spremnik)
        {
            if (id != spremnik.SpremnikId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(spremnik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpremnikExists(spremnik.SpremnikId))
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
            ViewData["BerbaId"] = new SelectList(_context.Berba, "BerbaId", "BerbaId", spremnik.BerbaId);
            ViewData["PodrumId"] = new SelectList(_context.Podrum, "PodrumId", "PodrumId", spremnik.PodrumId);
            ViewData["PunilacId"] = new SelectList(_context.Zaposlenik, "ZaposlenikId", "KorisnickoIme", spremnik.PunilacId);
            ViewData["SortaVinaId"] = new SelectList(_context.SortaVina, "SortaVinaId", "SortaVinaId", spremnik.SortaVinaId);
            ViewData["VrstaSpremnikaId"] = new SelectList(_context.VrstaSpremnika, "VrstaSpremnikaId", "VrstaSpremnikaId", spremnik.VrstaSpremnikaId);
            return View(spremnik);
        }

        // GET: Spremnici/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spremnik = await _context.Spremnik
                .Include(s => s.Berba)
                .Include(s => s.Podrum)
                .Include(s => s.Punilac)
                .Include(s => s.SortaVina)
                .Include(s => s.VrstaSpremnika)
                .FirstOrDefaultAsync(m => m.SpremnikId == id);
            if (spremnik == null)
            {
                return NotFound();
            }

            return View(spremnik);
        }

        // POST: Spremnici/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var spremnik = await _context.Spremnik.FindAsync(id);
            _context.Spremnik.Remove(spremnik);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpremnikExists(int id)
        {
            return _context.Spremnik.Any(e => e.SpremnikId == id);
        }

        //public IActionResult Filter(SpremniciFilter filter)
        //{
        //    return RedirectToAction("Index", new {filter = filter});
        //}

        public JsonResult CheckCode(SpremnikIM spremnikInput)
        {
            bool exists = _context.Spremnik.Any(s => s.ŠifraSpremnika == spremnikInput.ŠifraSpremnika);

            return Json(!exists);
        }

        public JsonResult CheckFillValue(SpremnikIM spremnikInput)
        {
            if (spremnikInput.Napunjenost > spremnikInput.Kapacitet)
            {
                return Json("Napunjenost ne može biti veća od kapaciteta!");
            }

            return Json(true);
        }

        public IActionResult GetSpremniciPodruma(string idPodrum)
        {
            int.TryParse(idPodrum, out int idP);

            var allSpremnici = _repository.GetAllSpremnici(idP)
                .OrderBy(s => s.ŠifraSpremnika)
                .ToList();

            return PartialView("GetSpremniciPodruma", allSpremnici);
        }
    }
}
