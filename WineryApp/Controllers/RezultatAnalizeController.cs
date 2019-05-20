using System;
using System.Linq;
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
            var allPodrumi = _repository.GetAllPodrumi();

            ViewData["Podrumi"] = new SelectList(allPodrumi, nameof(Podrum.PodrumId), nameof(Podrum.ŠifraPodruma));

            var model = new RezultatAnalizeViewModel
            {
                RezultatiAnalize = allRezultatiAnalize,
                Zaposlenici = allZaposlenici
            };

            return View(model);
        }

        // GET: RezultatAnalize/Details/5
        public IActionResult Details(int? id, string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl)) ViewData["returnUrl"] = returnUrl;

            if (id == null)
            {
                return NotFound();
            }

            var rezultatAnalize = _repository.GetRezultatAnalize(id.Value);

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
                var noviRezultatAnalize = _mapper.ToRezultatAnalize(rezultatAnalizeInput, _repository);

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
        public IActionResult Edit(int? id, string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl)) ViewData["returnUrl"] = returnUrl;

            if (id == null)
            {
                return NotFound();
            }

            var rezultatAnalize = _repository.GetRezultatAnalize(id.Value);

            if (rezultatAnalize == null)
            {
                return NotFound();
            }

            var allZaposlenici = _repository.GetAllZaposleniciBezVlasnika();
            var allPodrumi = _repository.GetAllPodrumi();
            var spremniciPodruma = _repository.GetAllSpremnici(rezultatAnalize.Spremnik.PodrumId);

            ViewData["Podrumi"] = new SelectList(allPodrumi, nameof(Podrum.PodrumId), nameof(Podrum.ŠifraPodruma));
            ViewData["Spremnici"] = new SelectList(spremniciPodruma, nameof(Spremnik.SpremnikId), nameof(Spremnik.ŠifraSpremnika));

            var model = new RezultatAnalizeViewModel
            {
                RezultatAnalizeInput = _mapper.ToRezultatAnalizeIM(rezultatAnalize),
                Zaposlenici = allZaposlenici
            };
            return View(model);
        }

        // POST: RezultatAnalize/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RezultatAnalizeIM rezultatAnalizeInput, string returnUrl)
        {
            if (id != rezultatAnalizeInput.RezultatAnalizeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var updateRezultatAnalize = _mapper.ToRezultatAnalize(rezultatAnalizeInput, _repository);
                try
                {
                    _context.Update(updateRezultatAnalize);
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    if (!RezultatAnalizeExists(updateRezultatAnalize.RezultatAnalizeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        TempData["Neuspješno"] = "Rezultat analize nije uspješno izmjenjen!";
                    }
                }
                TempData["Uspješno"] = "Rezultat analize je uspješno izmijenjen!";

                if (!string.IsNullOrEmpty(returnUrl)) return Redirect(returnUrl);

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Edit");
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
        public IActionResult GetSpremniciPodruma(string idPodrum)
        {
            int.TryParse(idPodrum, out int idP);

            var allSpremnici = _repository.GetAllSpremnici(idP)
                .ToList();

            return PartialView("GetSpremniciPodruma", allSpremnici);
        }

        public IActionResult Nazad(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl)) return Redirect(returnUrl);
            return RedirectToAction("Index");
        }
    }
}
