using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WineryApp.Data;
using WineryApp.Data.Entiteti;
using WineryApp.ViewModels.Zaposlenici;

namespace WineryApp.Controllers
{
    public class ZaposlenikController : Controller
    {
        private readonly WineryAppDbContext _context;
        private readonly IRepository _repository;
        private readonly UserManager<IdentityUser> _userManager;

        public ZaposlenikController(WineryAppDbContext context, IRepository repository, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _repository = repository;
            _userManager = userManager;
        }

        // GET: Zaposlenik
        public IActionResult Index()
        {
            var allZaposleniciBezVlasnika =
                _repository.GetAllZaposlenici().FindAll(z => z.UlogaId == (int)Uloge.Zaposlenik);

            var model = new ZaposleniciViewModel
            {
                Zaposlenici = allZaposleniciBezVlasnika
            };
            return View(model);
        }

        // GET: Zaposlenik/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaposlenik = _context.Zaposlenik
                .Include(z => z.Uloga)
                .Include(z => z.Zadatak)
                .FirstOrDefault(m => m.ZaposlenikId == id);

            if (zaposlenik == null)
            {
                return NotFound();
            }

            return View(zaposlenik);
        }


        [HttpPost]
        public async Task<IActionResult> DodajZaposlenika(ZaposlenikIM zaposlenikInput)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = zaposlenikInput.KorisnickoIme, Email = zaposlenikInput.Email };
                var result = await _userManager.CreateAsync(user, zaposlenikInput.Lozinka);
                if (result.Succeeded)
                {
                    var noviZaposlenik = new Zaposlenik
                    {
                        Ime = zaposlenikInput.Ime,
                        Prezime = zaposlenikInput.Prezime,
                        Email = zaposlenikInput.Email,
                        Adresa = zaposlenikInput.Adresa,
                        DatumZaposlenja = DateTime.Today,
                        Grad = zaposlenikInput.Grad,
                        KorisnickoIme = zaposlenikInput.KorisnickoIme,
                        Lozinka = zaposlenikInput.Lozinka,
                        Spol = zaposlenikInput.Spol,
                        Telefon = zaposlenikInput.Telefon,
                        UlogaId = 2,
                        User = user
                    };
                    _repository.DodajZaposlenika(noviZaposlenik);

                    TempData["Uspješno"] = $"Zaposlenik {noviZaposlenik.Ime} {noviZaposlenik.Prezime} uspješno dodan!";
                }
                else
                {
                    TempData["Neuspješno"] = $"Zaposlenik {zaposlenikInput.Ime} {zaposlenikInput.Prezime} nije uspješno dodan!";
                }
            }
            else
            {
                TempData["Neuspješno"] = $"Zaposlenik {zaposlenikInput.Ime} {zaposlenikInput.Prezime} nije uspio biti dodan!";
            }

            return RedirectToAction("Index");
        }

        // GET: Zaposlenik/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaposlenik = await _context.Zaposlenik.FindAsync(id);
            if (zaposlenik == null)
            {
                return NotFound();
            }

            ViewData["UlogaId"] = new SelectList(_context.Uloga, "UlogaId", "NazivUloga", zaposlenik.UlogaId);
            return View(zaposlenik);
        }

        // POST: Zaposlenik/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ZaposlenikId,Ime,Prezime,Spol,Adresa,Grad,Telefon,Email,Lozinka,DatumZaposlenja,KorisnickoIme,UlogaId")] Zaposlenik zaposlenik)
        {
            if (id != zaposlenik.ZaposlenikId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zaposlenik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZaposlenikExists(zaposlenik.ZaposlenikId))
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
            ViewData["UlogaId"] = new SelectList(_context.Uloga, "UlogaId", "UlogaId", zaposlenik.UlogaId);
            return View(zaposlenik);
        }

        // GET: Zaposlenik/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaposlenik = await _context.Zaposlenik
                .Include(z => z.Uloga)
                .FirstOrDefaultAsync(m => m.ZaposlenikId == id);
            if (zaposlenik == null)
            {
                return NotFound();
            }

            return View(zaposlenik);
        }

        // POST: Zaposlenik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zaposlenik = _context.Zaposlenik.Include(z => z.User).First(z => z.ZaposlenikId == id);
            _context.Zaposlenik.Remove(zaposlenik);
            await _userManager.DeleteAsync(zaposlenik.User);
            await _context.SaveChangesAsync();
            TempData["Uspješno"] = $"Zaposlenik {zaposlenik.Ime} {zaposlenik.Prezime} uspješno obrisan!";
            return RedirectToAction(nameof(Index));
        }

        private bool ZaposlenikExists(int id)
        {
            return _context.Zaposlenik.Any(e => e.ZaposlenikId == id);
        }

        public JsonResult ProvjeraEmailAdrese(ZaposlenikIM zaposlenikInput)
        {
            var email = zaposlenikInput.Email.ToLower();
            return Json(_repository.ProvjeraEmailAdrese(email));
        }
    }
}
