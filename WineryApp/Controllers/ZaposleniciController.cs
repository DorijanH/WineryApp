using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WineryApp.Data;
using WineryApp.Data.Entiteti;
using WineryApp.ViewModels.Zaposlenici;

namespace WineryApp.Controllers
{
    public class ZaposleniciController : Controller
    {
        private readonly WineryAppDbContext _context;
        private readonly IRepository _repository;
        private readonly UserManager<IdentityUser> _userManager;

        public ZaposleniciController(WineryAppDbContext context, IRepository repository, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _repository = repository;
            _userManager = userManager;
        }

        // GET: Zaposlenik
        public IActionResult Index(string filter)
        {
            List<Zaposlenik> zaposlenici;

            if (!string.IsNullOrWhiteSpace(filter))
            {
                var upit = _context.Zaposlenik
                    .Include(z => z.PovijestAditiva)
                    .Include(z => z.PovijestSpremnika)
                    .Include(z => z.RezultatAnalize)
                    .Include(z => z.Spremnik)
                    .Include(z => z.Zadatak)
                    .Where(z => z.UlogaId == (int)Uloge.Zaposlenik)
                    .OrderBy(z => z.Prezime)
                    .AsNoTracking();

                ZaposleniciFilter zf = new ZaposleniciFilter();
                zf = ZaposleniciFilter.FromString(filter);

                if (!zf.IsEmpty())
                {
                    upit = zf.PrimjeniFilter(upit);
                }

                zaposlenici = upit.ToList();
            }
            else
            {
                zaposlenici = _repository.GetAllZaposleniciBezVlasnika();
            }

            var model = new ZaposleniciViewModel
            {
                Zaposlenici = zaposlenici
            };

            return View(model);
        }

        public IActionResult Filter(ZaposleniciFilter filter)
        {
            return RedirectToAction(nameof(Index), new { filter = filter.ToString() });
        }

        // GET: Zaposlenik/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zaposlenik = _repository.GetZaposlenik(id.Value);

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
        public async Task<IActionResult> Edit(int? id, string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl)) ViewData["returnUrl"] = returnUrl;

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
        public async Task<IActionResult> Edit(int id, [Bind("ZaposlenikId,Ime,Prezime,Spol,Adresa,Grad,Telefon,Email,Lozinka,DatumZaposlenja,KorisnickoIme,UlogaId")] Zaposlenik zaposlenik, string returnUrl)
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
                catch (Exception)
                {
                    if (!ZaposlenikExists(zaposlenik.ZaposlenikId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        TempData["Neuspješno"] = "Zaposlenik nije uspješno izmjenjen!";
                    }
                }
                TempData["Uspješno"] = "Zaposlenik je uspješno izmijenjen!";

                if (!string.IsNullOrEmpty(returnUrl)) return Redirect(returnUrl);

                return RedirectToAction(nameof(Index));
            }
            ViewData["UlogaId"] = new SelectList(_context.Uloga, "UlogaId", "UlogaId", zaposlenik.UlogaId);
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

        public IActionResult Nazad(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl)) return Redirect(returnUrl);
            return RedirectToAction("Index");
        }
    }
}
