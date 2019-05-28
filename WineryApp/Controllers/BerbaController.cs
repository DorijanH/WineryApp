﻿using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WineryApp.Data;
using WineryApp.Data.Entiteti;
using WineryApp.ViewModels.Berbe;

namespace WineryApp.Controllers
{
    public class BerbaController : Controller
    {
        private readonly WineryAppDbContext _context;
        private readonly IRepository _repository;

        public BerbaController(WineryAppDbContext context, IRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        // GET: Berba
        public IActionResult Index()
        {
            var allBerbe = _repository.GetAllBerba();

            var model = new BerbeViewModel
            {
                Berbe = allBerbe
            };

            return View(model);
        }

        // GET: Berba/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var berba = await _context.Berba
                .FirstOrDefaultAsync(m => m.BerbaId == id);
            if (berba == null)
            {
                return NotFound();
            }

            return View(berba);
        }

        // POST: Berba/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DodajBerbu(Berba berbaInput)
        {
            if (ModelState.IsValid)
            {
                _context.Add(berbaInput);
                await _context.SaveChangesAsync();
                TempData["Uspješno"] = $"Godina berbe {berbaInput.GodinaBerbe} uspješno dodana!";
                return RedirectToAction(nameof(Index));
            }
            return View("Index");
        }

        // GET: Berba/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var berba = await _context.Berba.FindAsync(id);
            if (berba == null)
            {
                return NotFound();
            }
            return View(berba);
        }

        // POST: Berba/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BerbaId,GodinaBerbe")] Berba berba)
        {
            if (id != berba.BerbaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(berba);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BerbaExists(berba.BerbaId))
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
            return View(berba);
        }

        // POST: Berba/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var berba = await _context.Berba.FindAsync(id);
            _context.Berba.Remove(berba);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BerbaExists(int id)
        {
            return _context.Berba.Any(e => e.BerbaId == id);
        }

        public JsonResult CheckYear(Berba berbaInput)
        {
            var godinaUnaprijed = DateTime.Today.AddYears(2).Year;
            var godinaUnazad = DateTime.Today.AddYears(-2).Year;

            if (berbaInput.GodinaBerbe > godinaUnaprijed || berbaInput.GodinaBerbe < godinaUnazad)
            {
                return Json("Maksimalno dopušteno odstupanje jest dvije godine od trenutne!");
            }

            return Json(true);
        }
    }
}
