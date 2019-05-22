using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WineryApp.Data;
using WineryApp.Data.Entiteti;
using WineryApp.ViewModels.Narudžbe;

namespace WineryApp.Controllers
{
    public class NarudžbeController : Controller
    {
        private readonly WineryAppDbContext _context;
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public NarudžbeController(WineryAppDbContext context, IRepository repository, IMapper mapper)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
        }

        // GET: Narudžbe
        public IActionResult Index()
        {
            var allNarudžbe = _repository.GetAllNarudžbe();

            var allPartneri = _repository.GetAllPartneri();
            ViewData["Partneri"] = new SelectList(allPartneri, nameof(Partner.PartnerId), nameof(Partner.ImePartnera));

            var allPodrumi = _repository.GetAllPodrumi();
            ViewData["Podrumi"] = new SelectList(allPodrumi, nameof(Podrum.PodrumId), nameof(Podrum.ŠifraPodruma));

            var model = new NarudžbeViewModel
            {
                Narudžbe = allNarudžbe
            };

            return View(model);
        }

        // GET: Narudžbe/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narudžba = await _context.Narudžba
                .Include(n => n.Partner)
                .Include(n => n.Spremnik)
                .FirstOrDefaultAsync(m => m.NarudzbaId == id);
            if (narudžba == null)
            {
                return NotFound();
            }

            return View(narudžba);
        }

        // POST: Narudžbe/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DodajNarudžbu(NarudžbaIM narudžbaInput)
        {
            if (ModelState.IsValid)
            {
                var narudžba = _mapper.ToNarudžba(narudžbaInput);

                narudžba.DatumNarudzbe = DateTime.Today;
                narudžba.Status = (int) StatusNarudžbe.Naručeno;

                _context.Add(narudžba);

                await _context.SaveChangesAsync();

                TempData["Uspješno"] = "Narudžba uspješno dodana!";

                return RedirectToAction(nameof(Index));
            }

            TempData["Neuspješno"] = "Narudžba nije uspješno dodana!";

            return View("Index");
        }

        // GET: Narudžbe/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narudžba = await _context.Narudžba.FindAsync(id);
            if (narudžba == null)
            {
                return NotFound();
            }
            ViewData["PartnerId"] = new SelectList(_context.Partner, "PartnerId", "PartnerId", narudžba.PartnerId);
            ViewData["SpremnikId"] = new SelectList(_context.Spremnik, "SpremnikId", "SpremnikId", narudžba.SpremnikId);
            return View(narudžba);
        }

        // POST: Narudžbe/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NarudzbaId,StatusId,DatumNarudzbe,DatumIsporuke,DatumNaplate,ImeKupca,PrezimeKupca,AdresaKupca,Količina,KonacnaCijena,SpremnikId,PartnerId")] Narudžba narudžba)
        {
            if (id != narudžba.NarudzbaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(narudžba);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NarudžbaExists(narudžba.NarudzbaId))
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
            ViewData["PartnerId"] = new SelectList(_context.Partner, "PartnerId", "PartnerId", narudžba.PartnerId);
            ViewData["SpremnikId"] = new SelectList(_context.Spremnik, "SpremnikId", "SpremnikId", narudžba.SpremnikId);
            return View(narudžba);
        }

        // POST: Narudžbe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var narudžba = await _context.Narudžba.FindAsync(id);
            _context.Narudžba.Remove(narudžba);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NarudžbaExists(int id)
        {
            return _context.Narudžba.Any(e => e.NarudzbaId == id);
        }

        public decimal GetCijenaVinaSpremnika(string idSpremnik)
        {
            int.TryParse(idSpremnik, out int idS);

            return _repository.GetCijenaVina(idS);
        }
    }
}
