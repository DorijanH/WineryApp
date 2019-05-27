using System.Collections.Generic;
using WineryApp.Data.Entiteti;

namespace WineryApp.ViewModels.Izvješća.Narudžbe
{
    public class IzvješćaNarudžbeViewModel
    {
        public NarudžbeFilterIzvješće Input { get; set; }

        public List<Partner> Partneri { get; set; }

        public List<Spremnik> Spremnici { get; set; }


    }
}
