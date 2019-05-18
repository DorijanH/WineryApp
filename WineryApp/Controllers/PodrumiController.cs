using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WineryApp.Data;
using WineryApp.Data.Entiteti;
using WineryApp.ViewModels.Podrumi;

namespace WineryApp.Controllers
{
    public class PodrumiController : Controller
    {
        private readonly WineryAppDbContext _context;
        private readonly IRepository _repository;

        public PodrumiController(WineryAppDbContext context, IRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        // GET: Podrumi
        public IActionResult Index(string filter)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                var upit = _context.Podrum
                    .Include(p => p.Spremnik)
                    .Include(p => p.PovijestAditiva)
                    .Include(p => p.Zadatak)
                    .AsNoTracking();

                PodrumiFilter pf = PodrumiFilter.FromString(filter, _repository);

                if (!pf.IsEmpty())
                {
                    upit = pf.PrimjeniFilter(upit);
                }

                var allPodrumi = upit.ToList();

                var allBerbe = _repository.GetAllBerba();
                var allSorte = _repository.GetAllSorteVina();

                ViewBag.Berbe = new SelectList(allBerbe, nameof(Berba.BerbaId), nameof(Berba.GodinaBerbe));
                ViewBag.Sorte = new SelectList(allSorte, nameof(SortaVina.SortaVinaId), nameof(SortaVina.NazivSorte));

                var model = new PodrumiIndexModel
                {
                    Podrumi = allPodrumi
                };

                return View(model);
            }
            else
            {
                var allPodrumi = _repository.GetAllPodrumi();

                var allBerbe = _repository.GetAllBerba();
                var allSorte = _repository.GetAllSorteVina();

                ViewBag.Berbe = new SelectList(allBerbe, nameof(Berba.BerbaId), nameof(Berba.GodinaBerbe));
                ViewBag.Sorte = new SelectList(allSorte, nameof(SortaVina.SortaVinaId), nameof(SortaVina.NazivSorte));

                var model = new PodrumiIndexModel
                {
                    Podrumi = allPodrumi
                };

                return View(model);
            }
        }

        // GET: Podrumi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var podrum = await _context.Podrum
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DodajPodrum(PodrumIM podrumInput)
        {
            if (ModelState.IsValid)
            {
                var noviPodrum = new Podrum
                {
                    ŠifraPodruma = podrumInput.ŠifraPodruma,
                    Lokacija = podrumInput.Lokacija,
                    Popunjenost = "0"
                };

                _context.Add(noviPodrum);

                await _context.SaveChangesAsync();

                TempData["Uspješno"] = $"Podrum {podrumInput.ŠifraPodruma} je uspješno dodan!";

                return RedirectToAction(nameof(Index));
            }
            ViewData["SortaVinaId"] = new SelectList(_context.SortaVina, "SortaVinaId", "SortaVinaId");
            return View("Create");
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
            ViewData["SortaVinaId"] = new SelectList(_context.SortaVina, "SortaVinaId", "SortaVinaId");
            return View(podrum);
        }

        // POST: Podrumi/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PodrumId,ŠifraPodruma,Popunjenost,FazaIzrade,Lokacija,SortaVinaId,RezultatAnalizeId")] Podrum podrum)
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
            ViewData["SortaVinaId"] = new SelectList(_context.SortaVina, "SortaVinaId", "SortaVinaId");
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

        [HttpPost]
        public IActionResult Filter(PodrumiFilter filter)
        {
            return RedirectToAction(nameof(Index), new { filter = filter.ToString() });
        }
    }
}
