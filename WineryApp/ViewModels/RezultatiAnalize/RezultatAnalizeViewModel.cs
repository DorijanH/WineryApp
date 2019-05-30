using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WineryApp.Data.Entiteti;

namespace WineryApp.ViewModels.RezultatiAnalize
{
    public class RezultatAnalizeViewModel
    {
        [BindProperty]
        public RezultatAnalizeIM RezultatAnalizeInput { get; set; }
        public List<RezultatAnalize> RezultatiAnalize { get; set; }
        public List<Zaposlenik> Zaposlenici { get; set; }
        public RezultatAnalizeFilter Filter { get; set; }
    }
}
