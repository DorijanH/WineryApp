using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WineryApp.Data;
using WineryApp.Data.Entiteti;
using WineryApp.ViewModels.Partneri;

namespace WineryApp.Controllers
{
    public class PartneriController : Controller
    {
        private readonly WineryAppDbContext _context;
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public PartneriController(WineryAppDbContext context, IRepository repository, IMapper mapper)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
        }

        // GET: Partneri
        public IActionResult Index()
        {
            var allPartneri = _repository.GetAllPartneri();

            var model = new PartneriViewModel
            {
                Partneri = allPartneri
            };

            return View(model);

        }

        // GET: Partneri/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partner = _repository.GetPartner(id.Value);

            if (partner == null)
            {
                return NotFound();
            }

            return View(partner);
        }

        // POST: Partneri/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DodajPartnera(PartnerIM partnerInput)
        {
            if (ModelState.IsValid)
            {
                var noviPartner = _mapper.ToPartner(partnerInput);

                _context.Add(noviPartner);

                await _context.SaveChangesAsync();

                TempData["Uspješno"] = $"Partner {partnerInput.ImePartnera} je uspješno dodan!";

            }
            else
            {
                TempData["Neuspješno"] = $"Partner {partnerInput.ImePartnera} nije uspješno dodan u bazu podataka!";
            }
            
            return RedirectToAction("Index");
        }

        // GET: Partneri/Edit/5
        public async Task<IActionResult> Edit(int? id, string returnUrl)
        {
            if (!string.IsNullOrWhiteSpace(returnUrl)) TempData["returnUrl"] = returnUrl;

            if (id == null)
            {
                return NotFound();
            }

            var partner = await _context.Partner.FindAsync(id);

            if (partner == null)
            {
                return NotFound();
            }

            var model = _mapper.ToPartnerIM(partner);

            return View(model);
        }

        // POST: Partneri/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PartnerId,ImePartnera,Oib,KontaktBroj,Adresa")] Partner partner)
        {
            if (id != partner.PartnerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(partner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartnerExists(partner.PartnerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        TempData["Neuspješno"] = "Partner nije uspješno izmjenjen!";
                    }
                }

                TempData["Uspješno"] = "Partner je uspješno izmjenjen!";

                return RedirectToAction(nameof(Index));
            }
            return View("Edit");
        }

        // POST: Partneri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var partner = await _context.Partner.FindAsync(id);
            _context.Partner.Remove(partner);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartnerExists(int id)
        {
            return _context.Partner.Any(e => e.PartnerId == id);
        }

        public IActionResult Nazad(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl)) return Redirect(returnUrl);
            return RedirectToAction("Index");
        }
    }
}
