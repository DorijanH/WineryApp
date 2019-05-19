using System.Collections.Generic;
using WineryApp.Data.Entiteti;

namespace WineryApp.ViewModels.Izvješća.Zadaci
{
    public class IzvješćaZadaciIViewModel
    {
        public ZadatakFilterIzvješće Input { get; set; }
        public List<KategorijaZadatka> KategorijeZadataka { get; set; }
        public List<Zaposlenik> Zaposlenici { get; set; }

    }
}
