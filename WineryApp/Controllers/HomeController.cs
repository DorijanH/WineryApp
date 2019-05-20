using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

        public HomeController(WineryAppDbContext context, IRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            var zadaciDanas = _repository.GetAllDanašnjiZadaci();

            var model = new HomeDashboardModel
            {
                ZadaciDanas = zadaciDanas
            };

            return View(model);
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