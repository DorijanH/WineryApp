using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WineryApp.Data;
using WineryApp.Data.Entiteti;
using WineryApp.ViewModels.Aditivi;

namespace WineryApp.Controllers
{
    public class AditiviController : Controller
    {
        private readonly WineryAppDbContext _context;
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public AditiviController(WineryAppDbContext context, IRepository repository, IMapper mapper)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
        }

        // GET: Aditivi
        public IActionResult Index(string filter)
        {
            var allVrsteAditiva = _repository.GetAllVrsteAditiva();

            ViewData["VrsteAditiva"] = new SelectList(allVrsteAditiva, nameof(VrstaAditiva.VrstaAditivaId), nameof(VrstaAditiva.NazivVrste));

            if (!string.IsNullOrEmpty(filter))
            {
                var upit = _context.Aditiv
                    .Include(a => a.VrstaAditiva)
                    .Include(a => a.PovijestAditiva)
                    .AsNoTracking();

                AditivFilter af = AditivFilter.FromString(filter);

                if (!af.IsEmpty())
                {
                    upit = af.PrimjeniFilter(upit);
                }

                var allAditivi = upit.ToList();
                
                var model = new AditiviViewModel
                {
                    Aditivi = allAditivi
                };

                return View(model);
            }
            else
            {
                var allAditivi = _repository.GetAllAditivi();

                var model = new AditiviViewModel
                {
                    Aditivi = allAditivi
                };

                return View(model);
            }
        }
        public IActionResult Filter(AditivFilter filter)
        {
            return RedirectToAction("Index", new { filter = filter.ToString() });
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
        public IActionResult DodajAditiv(AditiviIM aditivInput)
        {
            if (ModelState.IsValid)
            {
                var noviAditiv = _mapper.ToAditiv(aditivInput);

                _context.Add(noviAditiv);

                _context.SaveChangesAsync();

                TempData["Uspješno"] = $"Aditiv {noviAditiv.ImeAditiva} je uspješno dodan!";

                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Neuspješno"] = "Aditiv nije uspješno dodan!";

                return RedirectToAction("Index");
            }
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

            TempData["Uspješno"] = $"Aditiv {aditiv.ImeAditiva} uspješno izbrisan!";

            return RedirectToAction(nameof(Index));
        }

        private bool AditivExists(int id)
        {
            return _context.Aditiv.Any(e => e.AditivId == id);
        }
    }
}
