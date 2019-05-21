using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WineryApp.Data.Entiteti;

namespace WineryApp.ViewModels.Zaposlenici
{
    public class ZaposleniciViewModel
    {
        [BindProperty]
        public ZaposlenikIM ZaposlenikInput { get; set; }
        public List<Zaposlenik> Zaposlenici { get; set; }

        public ZaposleniciFilter Filter { get; set; }
    }
}
