using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using WineryApp.Data;
using WineryApp.Data.Entiteti;
using WineryApp.ViewModels.Spremnici;

namespace WineryApp.Controllers
{
    public class SpremniciController : Controller
    {
        private readonly WineryAppDbContext _context;
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public SpremniciController(WineryAppDbContext context, IRepository repository, IMapper mapper)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
        }

        // GET: Spremnici
        public IActionResult Index(string filter)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                var upit = _repository.GetAllSpremnici()
                    .OrderBy(s => s.ŠifraSpremnika)
                    .AsQueryable();

                SpremnikFilter sf = SpremnikFilter.FromString(filter);

                if (!sf.IsEmpty())
                {
                    upit = sf.PrimjeniFilter(upit);
                }

                var allSpremnici = upit.ToList();
                var allZaposlenici = _repository.GetAllZaposlenici().Where(z => z.UlogaId == (int)Uloge.Zaposlenik).ToList();

                ViewData["VrsteSpremnika"] = new SelectList(_context.VrstaSpremnika, nameof(VrstaSpremnika.VrstaSpremnikaId), nameof(VrstaSpremnika.NazivVrste));
                ViewData["Berbe"] = new SelectList(_context.Berba, nameof(Berba.BerbaId), nameof(Berba.GodinaBerbe));
                ViewData["Podrumi"] = new SelectList(_context.Podrum, nameof(Podrum.PodrumId), nameof(Podrum.ŠifraPodruma));
                ViewData["Sorte"] = new SelectList(_context.SortaVina.OrderBy(sv => sv.NazivSorte), nameof(SortaVina.SortaVinaId), nameof(SortaVina.NazivSorte));

                var model = new SpremniciViewModel
                {
                    Spremnici = allSpremnici,
                    Zaposlenici = allZaposlenici
                };

                return View(model);
            }
            else
            {
                var allSpremnici = _repository.GetAllSpremnici();
                var allZaposlenici = _repository.GetAllZaposlenici()
                    .Where(z => z.UlogaId == (int)Uloge.Zaposlenik)
                    .OrderBy(z => z.Prezime)
                    .ToList();

                ViewData["VrsteSpremnika"] = new SelectList(_context.VrstaSpremnika, nameof(VrstaSpremnika.VrstaSpremnikaId), nameof(VrstaSpremnika.NazivVrste));
                ViewData["Berbe"] = new SelectList(_context.Berba, nameof(Berba.BerbaId), nameof(Berba.GodinaBerbe));
                ViewData["Podrumi"] = new SelectList(_context.Podrum, nameof(Podrum.PodrumId), nameof(Podrum.ŠifraPodruma));
                ViewData["Sorte"] = new SelectList(_context.SortaVina.OrderBy(sv => sv.NazivSorte), nameof(SortaVina.SortaVinaId), nameof(SortaVina.NazivSorte));

                var model = new SpremniciViewModel
                {
                    Spremnici = allSpremnici,
                    Zaposlenici = allZaposlenici
                };

                return View(model);
            }
            
        }

        // GET: Spremnici/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spremnik = _repository.GetSpremnik(id.Value);

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
                var noviSpremnik = _mapper.ToSpremnik(spremnikInput);

                _context.Add(noviSpremnik);
                _context.SaveChangesAsync();
                TempData["Uspješno"] = $"Spremnik {spremnikInput.ŠifraSpremnika} uspješno dodan!";
                return RedirectToAction("Index");
            }

            TempData["Neuspješno"] = $"Spremnik {spremnikInput.ŠifraSpremnika} nije mogao biti dodan!";
            return View("Index");
        }

        // GET: Spremnici/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spremnik = _repository.GetSpremnik(id.Value);
            var allZaposlenici = _repository.GetAllZaposlenici()
                .Where(z => z.UlogaId == (int) Uloge.Zaposlenik)
                .OrderBy(z => z.Prezime).ToList();


            if (spremnik == null)
            {
                return NotFound();
            }

            ViewData["Berbe"] = new SelectList(_context.Berba, nameof(Berba.BerbaId), nameof(Berba.GodinaBerbe));
            ViewData["Podrumi"] = new SelectList(_context.Podrum.OrderBy(p => p.ŠifraPodruma), nameof(Podrum.PodrumId), nameof(Podrum.ŠifraPodruma));
            ViewData["SorteVina"] = new SelectList(_context.SortaVina.OrderBy(sv => sv.NazivSorte), nameof(SortaVina.SortaVinaId), nameof(SortaVina.NazivSorte));
            ViewData["VrsteSpremnika"] = new SelectList(_context.VrstaSpremnika.OrderBy(vs => vs.NazivVrste), nameof(VrstaSpremnika.VrstaSpremnikaId), nameof(VrstaSpremnika.NazivVrste));

            var model = new SpremniciViewModel
            {
                SpremnikInput = _mapper.ToSpremnikIM(spremnik),
                Zaposlenici = allZaposlenici
            };

            return View(model);
        }

        // POST: Spremnici/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SpremnikIM spremnikInput)
        {
            if (id != spremnikInput.SpremnikId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var updateSpremnik = _mapper.ToSpremnik(spremnikInput);
                try
                {
                    _context.Update(updateSpremnik);
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    if (!SpremnikExists(spremnikInput.SpremnikId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        TempData["Neuspješno"] = "Spremnik nije uspješno izmjenjen!";
                    }
                }
                TempData["Uspješno"] = "Spremnik je uspješno izmjenjen!";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var spremnik = _repository.GetSpremnik(id);
                var allZaposlenici = _repository.GetAllZaposlenici()
                    .Where(z => z.UlogaId == (int)Uloge.Zaposlenik)
                    .OrderBy(z => z.Prezime).ToList();

                ViewData["Berbe"] = new SelectList(_context.Berba, nameof(Berba.BerbaId), nameof(Berba.GodinaBerbe));
                ViewData["Podrumi"] = new SelectList(_context.Podrum.OrderBy(p => p.ŠifraPodruma), nameof(Podrum.PodrumId), nameof(Podrum.ŠifraPodruma));
                ViewData["SorteVina"] = new SelectList(_context.SortaVina.OrderBy(sv => sv.NazivSorte), nameof(SortaVina.SortaVinaId), nameof(SortaVina.NazivSorte));
                ViewData["VrsteSpremnika"] = new SelectList(_context.VrstaSpremnika.OrderBy(vs => vs.NazivVrste), nameof(VrstaSpremnika.VrstaSpremnikaId), nameof(VrstaSpremnika.NazivVrste));

                var model = new SpremniciViewModel
                {
                    SpremnikInput = _mapper.ToSpremnikIM(spremnik),
                    Zaposlenici = allZaposlenici
                };

                return View(model);
            }
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

        public IActionResult Filter(SpremnikFilter filter)
        {
            return RedirectToAction("Index", new { filter = filter.ToString() });
        }

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

        public string GetOpisVrsteSpremnika(string idVrsta)
        {
            int.TryParse(idVrsta, out int idV);

            var opis = _context.VrstaSpremnika.Find(idV).Opis;
            return opis;
        }
    }
}
