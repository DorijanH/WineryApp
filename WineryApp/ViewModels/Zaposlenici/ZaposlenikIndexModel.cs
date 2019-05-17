using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WineryApp.Data.Entiteti;

namespace WineryApp.ViewModels.Zaposlenici
{
    public class ZaposlenikIndexModel
    {
        [BindProperty]
        public ZaposlenikIM ZaposlenikInput { get; set; }
        public List<Zaposlenik> Zaposlenici { get; set; }
    }
}
