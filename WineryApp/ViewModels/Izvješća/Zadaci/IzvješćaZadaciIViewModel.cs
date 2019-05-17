using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WineryApp.Data.Entiteti;
using WineryApp.ViewModels.Zadaci;

namespace WineryApp.ViewModels.Izvješća
{
    public class IzvješćaZadaciIViewModel
    {
        public ZadatakFilterIzvješće Input { get; set; }
        public List<KategorijaZadatka> KategorijeZadataka { get; set; }
        public List<Zaposlenik> Zaposlenici { get; set; }

    }
}
