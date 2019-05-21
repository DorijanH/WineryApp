using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WineryApp.Data;
using WineryApp.Data.Entiteti;
using WineryApp.ViewModels.Zadaci;

namespace WineryApp.Controllers
{
    public class ZadaciController : Controller
    {
        private readonly WineryAppDbContext _context;
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public ZadaciController(WineryAppDbContext context, IRepository repository, IMapper mapper)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
        }

        // GET: Zadatak
        public IActionResult Index(string filter)
        {
            if (!string.IsNullOrWhiteSpace(filter))
            {
                var upit = _context.Zadatak
                    .Include(z => z.ZaduženiZaposlenikNavigation)
                    .Include(z => z.KategorijaZadatka)
                    .Include(z => z.Podrum)
                    .Include(z => z.Spremnik)
                    .AsNoTracking();

                ZadaciFilter zf = new ZadaciFilter();
                zf = ZadaciFilter.FromString(filter);

                if (!zf.IsEmpty())
                {
                    upit = zf.PrimjeniFilter(upit);
                }

                var allZadaci = upit.ToList();

                var allKategorijeZadataka = _repository.GetAllKategorijeZadataka();
                var allZaposleniciBezVlasnika = _repository.GetAllZaposleniciBezVlasnika();
                var allPodrumi = _repository.GetAllPodrumi();
                var allVrsteAditiva = _repository.GetAllVrsteAditiva();

                if (allPodrumi.Count > 0)
                {
                    ViewBag.Podrumi = new SelectList(allPodrumi, nameof(Podrum.PodrumId), nameof(Podrum.ŠifraPodruma));
                }

                ViewBag.VrsteAditiva = new SelectList(allVrsteAditiva, nameof(VrstaAditiva.VrstaAditivaId), nameof(VrstaAditiva.NazivVrste));

                var model = new ZadaciViewModel
                {
                    Zadaci = allZadaci,
                    KategorijeZadataka = allKategorijeZadataka,
                    Zaposlenici = allZaposleniciBezVlasnika
                };

                return View(model);
            }
            else
            {
                var allZadaci = _repository.GetAllZadaci();
                var allKategorijeZadataka = _repository.GetAllKategorijeZadataka();
                var allZaposleniciBezVlasnika = _repository.GetAllZaposleniciBezVlasnika();
                var allPodrumi = _repository.GetAllPodrumi();
                var allVrsteAditiva = _repository.GetAllVrsteAditiva();

                ViewBag.Podrumi = new SelectList(allPodrumi, nameof(Podrum.PodrumId), nameof(Podrum.ŠifraPodruma));
                ViewBag.VrsteAditiva = new SelectList(allVrsteAditiva, nameof(VrstaAditiva.VrstaAditivaId), nameof(VrstaAditiva.NazivVrste));

                var model = new ZadaciViewModel
                {
                    Zadaci = allZadaci,
                    KategorijeZadataka = allKategorijeZadataka,
                    Zaposlenici = allZaposleniciBezVlasnika
                };

                return View(model);
            }
            
        }

        // GET: Zadatak/Details/5
        public async Task<IActionResult> Details(int? id, string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl)) ViewData["returnUrl"] = returnUrl;

            if (id == null)
            {
                return NotFound();
            }

            var zadatak = await _context.Zadatak
                .Include(z => z.KategorijaZadatka)
                .Include(z => z.ZaduženiZaposlenikNavigation)
                .Include(z => z.Spremnik)
                .Include(z => z.Podrum)
                .FirstOrDefaultAsync(m => m.ZadatakId == id);

            if (zadatak == null)
            {
                return NotFound();
            }

            return View(zadatak);
        }


        [HttpPost]
        public IActionResult DodajZadatak(ZadatakIM zadatakInput)
        {
            if (ModelState.IsValid || (!zadatakInput.VrstaAditivaId.HasValue && !zadatakInput.AditivId.HasValue))
            {
                var noviZadatak = new Zadatak
                {
                    ImeZadatka = zadatakInput.ImeZadatka,
                    PočetakZadatka = zadatakInput.PočetakZadatka,
                    RokZadatka = zadatakInput.RokZadatka,
                    PodrumId = zadatakInput.PodrumId,
                    AditivId = zadatakInput.AditivId,
                    Podrum = zadatakInput.PodrumId.HasValue ? _repository.GetPodrum(zadatakInput.PodrumId.Value) : null,
                    SpremnikId = zadatakInput.SpremnikId,
                    Spremnik = zadatakInput.SpremnikId.HasValue ? _repository.GetSpremnik(zadatakInput.SpremnikId.Value) : null,
                    KategorijaZadatkaId = zadatakInput.KategorijaZadatkaId,
                    KategorijaZadatka = _repository.GetKategorijaZadatka(zadatakInput.KategorijaZadatkaId),
                    Bilješke = zadatakInput.Bilješke,
                    ZaduženiZaposlenik = zadatakInput.ZaposlenikId,
                    ZaduženiZaposlenikNavigation = _repository.GetZaposlenik(zadatakInput.ZaposlenikId),
                    StatusZadatka = (int) StatusZadatka.UTijeku
                };

                _repository.DodajZadatak(noviZadatak);
                TempData["Uspješno"] = "Zadatak je uspješno zadan!";

                StvoriPorukuNoviZadatak(noviZadatak);
            }
            else
            {
                TempData["Neuspješno"] = "Zadatak nije uspješno dodan u bazu podataka";
            }
            return RedirectToAction("Index");
        }

        // GET: Zadatak/Edit/5
        public async Task<IActionResult> Edit(int? id, string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl)) ViewData["returnUrl"] = returnUrl;

            if (id == null)
            {
                return NotFound();
            }

            var zadatak = _repository.GetZadatak(id.Value);

            var zadatakInput = _mapper.ToZadatakIM(zadatak, _repository);

            if (zadatak == null)
            {
                return NotFound();
            }

            var allZaposleniciBezVlasnika = _repository.GetAllZaposleniciBezVlasnika();
            var allPodrumi = _repository.GetAllPodrumi();
            var allVrsteAditiva = _repository.GetAllVrsteAditiva();

            if (zadatakInput.VrstaAditivaId.HasValue)
            {
                var aditiviVrste = _repository.GetAllAditivi(zadatakInput.VrstaAditivaId.Value);
                ViewBag.Aditivi = new SelectList(aditiviVrste, nameof(Aditiv.AditivId), nameof(Aditiv.ImeAditiva));
            }

            if (zadatak.PodrumId.HasValue)
            {
                var spremniciPodruma = _repository.GetAllSpremnici(zadatak.PodrumId.Value);
                ViewBag.Spremnici = new SelectList(spremniciPodruma, nameof(Spremnik.SpremnikId), nameof(Spremnik.ŠifraSpremnika));
            }

            ViewBag.VrsteAditiva = new SelectList(allVrsteAditiva, nameof(VrstaAditiva.VrstaAditivaId), nameof(VrstaAditiva.NazivVrste));
            ViewBag.Podrumi = new SelectList(allPodrumi, nameof(Podrum.PodrumId), nameof(Podrum.ŠifraPodruma));
            ViewBag.KategorijeZadatka = new SelectList(_context.KategorijaZadatka, nameof(KategorijaZadatka.KategorijaZadatkaId), nameof(KategorijaZadatka.ImeKategorije));

            var model = new ZadaciViewModel
            {
                ZadatakInput = zadatakInput,
                Zaposlenici = allZaposleniciBezVlasnika
            };

            return View(model);
        }

        // POST: Zadatak/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ZadatakIM zadatakInput, string returnUrl)
        {
            if (id != zadatakInput.ZadatakId)
            {
                return NotFound();
            }

            if (ModelState.IsValid || (!zadatakInput.VrstaAditivaId.HasValue && !zadatakInput.AditivId.HasValue))
            {
                var updateZadatak = _mapper.ToZadatak(zadatakInput);
                try
                {
                    _context.Update(updateZadatak);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    if (!ZadatakExists(updateZadatak.ZadatakId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        TempData["Neuspješno"] = "Zadatak nije uspješno izmjenjen!";
                    }
                }

                if (updateZadatak.StatusZadatka == (int) StatusZadatka.Zavrseno && (updateZadatak.PodrumId.HasValue && updateZadatak.SpremnikId.HasValue))
                {
                    _repository.AddPovijestSpremnika(updateZadatak.ZadatakId);
                }

                if (updateZadatak.StatusZadatka == (int)StatusZadatka.Zavrseno && updateZadatak.AditivId.HasValue)
                {
                    _repository.AddPovijestAditiva(updateZadatak.ZadatakId, zadatakInput.IskorištenaKoličina);
                }

                TempData["Uspješno"] = "Zadatak je uspješno izmjenjen!";

                if (!string.IsNullOrEmpty(returnUrl)) return Redirect(returnUrl);

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Edit");
        }

        // POST: Zadatak/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zadatak = await _context.Zadatak.FindAsync(id);
            _context.Zadatak.Remove(zadatak);
            await _context.SaveChangesAsync();

            TempData["Uspješno"] = $"Zadatak {zadatak.ImeZadatka} uspješno izbrisan!";

            return RedirectToAction(nameof(Index));
        }

        private bool ZadatakExists(int id)
        {
            return _context.Zadatak.Any(e => e.ZadatakId == id);
        }

        private void StvoriPorukuNoviZadatak(Zadatak zadatak)
        {
            var zaposlenik = zadatak.ZaduženiZaposlenikNavigation;

            string messageSubjekt = $"{zaposlenik.Ime} - Novi zadatak!";

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Bok {zaposlenik.Ime}!");
            sb.AppendLine();
            sb.AppendLine("Tvoj novi zadatak je: ");
            sb.AppendLine();
            sb.AppendLine($"Ime zadatka: {zadatak.ImeZadatka}");
            sb.AppendLine($"Vrsta zadatka: {zadatak.KategorijaZadatka.ImeKategorije}");
            sb.AppendLine($"Šifra podruma: {(zadatak.PodrumId.HasValue ? zadatak.Podrum.ŠifraPodruma : "Općenito")}");
            sb.AppendLine($"Šifra spremnika: {(zadatak.SpremnikId.HasValue ? zadatak.Spremnik.ŠifraSpremnika : "Općenito")}");
            sb.AppendLine("Napomene:");
            sb.AppendLine(zadatak.Bilješke);
            sb.AppendLine();
            sb.AppendLine($"Početni datum zadatka: {zadatak.PočetakZadatka:d/M/yyyy}");
            sb.AppendLine($"Rok zadatka: {zadatak.RokZadatka:d/M/yyyy}");
            sb.AppendLine();
            sb.AppendLine("Pozdrav, ");
            sb.AppendLine("Tvoj WineryApp!");

            _repository.SendEmail(zaposlenik, messageSubjekt, sb.ToString());
        }

        [HttpPost]
        public IActionResult Filter(ZadaciFilter filter)
        {
            return RedirectToAction(nameof(Index), new { filter = filter.ToString() });
        }

        public JsonResult ValidDate(ZadatakIM zadatakInput)
        {
            if (zadatakInput.RokZadatka != DateTime.MinValue)
            {
                if (zadatakInput.PočetakZadatka >= DateTime.Today && zadatakInput.RokZadatka >= zadatakInput.PočetakZadatka)
                {
                    return Json(true);
                }

                return Json("Datum je već prošao ili je postavljen prije početka zadatka!");
            }
            if (zadatakInput.PočetakZadatka >= DateTime.Today)
            {
                return Json(true);
            }

            return Json(false);
        }

        public IActionResult Nazad(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl)) return Redirect(returnUrl);
            return RedirectToAction("Index");
        }
    }
}
