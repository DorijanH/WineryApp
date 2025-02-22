﻿using System;
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
        public IActionResult Index(string filter)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                var upit = _repository.GetAllNarudžbe()
                    .AsQueryable();

                NarudžbaFilter nf = NarudžbaFilter.FromString(filter);

                if (!nf.IsEmpty())
                {
                    upit = nf.PrimjeniFilter(upit);
                }

                var allNarudžbe = upit.ToList();

                var allPartneri = _repository.GetAllPartneri();
                ViewData["Partneri"] = new SelectList(allPartneri, nameof(Partner.PartnerId), nameof(Partner.ImePartnera));

                var allPodrumi = _repository.GetAllPodrumi();
                ViewData["Podrumi"] = new SelectList(allPodrumi, nameof(Podrum.PodrumId), nameof(Podrum.ŠifraPodruma));

                var allSpremnici = _repository.GetAllSpremnici();
                ViewData["Spremnici"] = new SelectList(allSpremnici, nameof(Spremnik.SpremnikId), nameof(Spremnik.ŠifraSpremnika));

                var model = new NarudžbeViewModel
                {
                    Narudžbe = allNarudžbe
                };

                return View(model);
            }
            else
            {
                var allNarudžbe = _repository.GetAllNarudžbe();

                var allPartneri = _repository.GetAllPartneri();
                ViewData["Partneri"] = new SelectList(allPartneri, nameof(Partner.PartnerId), nameof(Partner.ImePartnera));

                var allPodrumi = _repository.GetAllPodrumi();
                ViewData["Podrumi"] = new SelectList(allPodrumi, nameof(Podrum.PodrumId), nameof(Podrum.ŠifraPodruma));

                var allSpremnici = _repository.GetAllSpremnici();
                ViewData["Spremnici"] = new SelectList(allSpremnici, nameof(Spremnik.SpremnikId), nameof(Spremnik.ŠifraSpremnika));

                var model = new NarudžbeViewModel
                {
                    Narudžbe = allNarudžbe
                };

                return View(model);
            }
        }

        // GET: Narudžbe/Details/5
        public IActionResult Details(int? id, string returnUrl)
        {

            if (!string.IsNullOrWhiteSpace(returnUrl)) ViewData["returnUrl"] = returnUrl;

            if (id == null)
            {
                return NotFound();
            }

            var narudžba = _repository.GetNarudžba(id.Value);


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
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narudžba = _repository.GetNarudžba(id.Value);

            var model = _mapper.ToNarudžbaIM(narudžba);

            var allSpremnici = _repository.GetAllSpremnici(model.PodrumId);
            ViewData["Spremnici"] =
                new SelectList(allSpremnici, nameof(Spremnik.SpremnikId), nameof(Spremnik.ŠifraSpremnika));

            var allPartneri = _repository.GetAllPartneri();
            ViewData["Partneri"] = new SelectList(allPartneri, nameof(Partner.PartnerId), nameof(Partner.ImePartnera));

            var allPodrumi = _repository.GetAllPodrumi();
            ViewData["Podrumi"] = new SelectList(allPodrumi, nameof(Podrum.PodrumId), nameof(Podrum.ŠifraPodruma));

            if (narudžba == null)
            {
                return NotFound();
            }
            
            return View(model);
        }

        // POST: Narudžbe/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NarudžbaIM input, string returnUrl)
        {
            if (id != input.NarudžbaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var narudžba = _mapper.ToNarudžba(input);

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
                        TempData["Neuspješno"] = "Narudžba nije uspješno izmjenjena!";
                    }
                }

                TempData["Uspješno"] = "Narudžba je uspješno izmjenjena!";

                if (!string.IsNullOrWhiteSpace(returnUrl)) return Redirect(returnUrl);

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Edit");
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

        public IActionResult Isporuči(int id, string returnUrl)
        {
            var rezultat = _repository.IsporučiNarudžbu(id);

            if (rezultat)
            {
                TempData["Uspješno"] = "Narudžba uspješno isporučena!";
            }
            else
            {
                TempData["Neuspješno"] = "Spremnik nema dovoljno vina! Narudžbu nije moguće isporučiti!";
            }

            return Redirect(returnUrl);
        }

        public IActionResult Naplati(int id, string returnUrl)
        {
            _repository.NaplatiNarudžbu(id);

            return Redirect(returnUrl);
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

        public IActionResult Nazad(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl)) return Redirect(returnUrl);
            return RedirectToAction("Index");
        }

        public IActionResult Filter(NarudžbaFilter filter)
        {
            return RedirectToAction("Index", new { filter = filter.ToString() });
        }
    }
}
