using System.Collections.Generic;
using WineryApp.Data.Entiteti;

namespace WineryApp.ViewModels.Izvješća.Spremnici
{
    public class IzvješćaSpremniciViewModel
    {
        public SpremnikFilterIzvješće Input { get; set; }
        public List<VrstaSpremnika> VrsteSpremnika { get; set; }
        public List<Podrum> Podrumi { get; set; }
        public List<Berba> Berbe { get; set; }
        public List<SortaVina> SorteVina { get; set; }
        public List<Zaposlenik> Zaposlenici { get; set; }
    }
}
