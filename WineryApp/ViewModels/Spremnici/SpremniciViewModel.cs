using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WineryApp.Data.Entiteti;

namespace WineryApp.ViewModels.Spremnici
{
    public class SpremniciViewModel
    {
        [BindProperty]
        public SpremnikIM SpremnikInput { get; set; }

        public List<Spremnik> Spremnici { get; set; }

        public List<Zaposlenik> Zaposlenici { get; set; }

        public SpremnikFilter Filter { get; set; }
    }
}
