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

                var model = new PodrumiViewModel
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

                var model = new PodrumiViewModel
                {
                    Podrumi = allPodrumi
                };

                return View(model);
            }
        }

        // GET: Podrumi/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var podrum = _context.Podrum
                .Include(p => p.Spremnik)
                .Include(p => p.Zadatak)
                .FirstOrDefault(m => m.PodrumId == id);

            if (podrum == null)
            {
                return NotFound();
            }

            return View(podrum);
        }

        // POST: Podrumi/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DodajPodrum(PodrumIM podrumInput)
        {
            if (ModelState.IsValid)
            {
                var noviPodrum = _mapper.ToPodrum(podrumInput);

                _context.Add(noviPodrum);

                await _context.SaveChangesAsync();

                TempData["Uspješno"] = $"Podrum {podrumInput.ŠifraPodruma} je uspješno dodan!";

                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Neuspješno"] = "Podrum nije uspješno dodan!";
                return RedirectToAction("Index");
            }
        }

        // GET: Podrumi/Edit/5
        public async Task<IActionResult> Edit(int? id, string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl)) ViewData["returnUrl"] = returnUrl;

            if (id == null)
            {
                return NotFound();
            }

            var podrum = await _context.Podrum.FindAsync(id);

            if (podrum == null)
            {
                return NotFound();
            }

            var model = _mapper.ToPodrumIM(podrum);

            return View(model);
        }

        // POST: Podrumi/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PodrumIM podrum, string returnUrl)
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
                catch (Exception)
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

                if (!string.IsNullOrEmpty(returnUrl)) return Redirect(returnUrl);

                return RedirectToAction(nameof(Index));
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

            TempData["Uspješno"] = $"Podrum {podrum.ŠifraPodruma} uspješno izbrisan!";

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

        public JsonResult CheckCode(PodrumIM podrumInput)
        {
            bool exists = _context.Podrum.Any(p => p.ŠifraPodruma == podrumInput.ŠifraPodruma);

            return Json(!exists);
        }

        public IActionResult Nazad(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl)) return Redirect(returnUrl);
            return RedirectToAction("Index");
        }
    }
}
