using System.Collections.Generic;
using WineryApp.Data.Entiteti;
using WineryApp.ViewModels.Izvješća.Spremnici;

namespace WineryApp.ViewModels.Izvješća.RezultatiAnalize
{
    public class IzvješćaRezultatiAnalizeViewModel
    {
        public RezultatAnalizeFilterIzvješće Input { get; set; }
        public List<Podrum> Podrumi { get; set; }
        public List<Spremnik> Spremnici { get; set; }
        public List<Zaposlenik> Zaposlenici { get; set; }
    }
}
