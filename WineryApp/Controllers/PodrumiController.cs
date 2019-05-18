using System;
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
        private readonly IMapper _mapper;

        public PodrumiController(WineryAppDbContext context, IRepository repository, IMapper mapper)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
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

            var model = _mapper.ToPodrumIM(podrum);

            return View(model);
        }

        // POST: Podrumi/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PodrumIM podrum)
        {
            if (id != podrum.PodrumId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var updatePodrum = _mapper.ToPodrum(podrum);
                try
                {
                    _context.Update(updatePodrum);
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    if (!PodrumExists(podrum.PodrumId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        TempData["Neuspješno"] = "Podrum nije uspješno izmjenjen!";
                    }
                }

                TempData["Uspješno"] = "Podrum je uspješno izmjenjen!";
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
