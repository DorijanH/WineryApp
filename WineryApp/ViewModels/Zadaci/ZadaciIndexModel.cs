using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WineryApp.Data.Entiteti;

namespace WineryApp.ViewModels.Zadaci
{
    public class ZadaciIndexModel
    {
        [BindProperty]
        public ZadatakIM ZadatakInput { get; set; }
        public List<Zadatak> Zadaci { get; set; }
        public List<KategorijaZadatka> KategorijeZadataka { get; set; }
        public List<Zaposlenik> Zaposlenici { get; set; }
        public List<Podrum> Podrumi { get; set; }
        public List<Spremnik> Spremnici { get; set; }
        public ZadaciFilter Filter { get; set; }

    }
}
