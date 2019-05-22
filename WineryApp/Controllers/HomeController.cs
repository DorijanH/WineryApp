using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WineryApp.Data;
using WineryApp.Models;
using WineryApp.ViewModels.Home;

namespace WineryApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly WineryAppDbContext _context;
        private readonly IRepository _repository;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(WineryAppDbContext context, IRepository repository, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _repository = repository;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            var userHash = _userManager.GetUserId(User);
            var korisnik = _repository.GetZaposlenik(userHash);

            if (_repository.AmIAdmin(userHash))
            {
                var zadaciDanas = _repository.GetAllDanašnjiZadaci();

                var povijestSpremnika = _repository.GetAllPovijestiSpremnika()
                    .Where(ps => ps.Datum >= DateTime.Today.AddDays(-7))
                    .ToList();

                var model = new HomeDashboardModel
                {
                    ZadaciDanas = zadaciDanas,
                    PovijestSTjedanDana = povijestSpremnika
                };

                return View(model);
            }
            else
            {
                var zadaciDanas = _repository.GetAllMojiZadaci(korisnik);

                var povijestSpremnika = _repository.GetAllPovijestiSpremnika()
                    .Where(ps => ps.Datum >= DateTime.Today.AddDays(-7))
                    .ToList();

                var model = new HomeDashboardModel
                {
                    ZadaciDanas = zadaciDanas,
                    PovijestSTjedanDana = povijestSpremnika
                };

                return View(model);
            }
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}