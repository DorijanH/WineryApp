using System.Collections.Generic;
using WineryApp.Data.Entiteti;
using WineryApp.ViewModels.Izvješća.Podrumi;

namespace WineryApp.ViewModels.Izvješća.Aditivi
{
    public class IzvješćaAditiviViewModel
    {
        public AditivFilterIzvješće Input { get; set; }
        public List<VrstaAditiva> VrsteAditiva { get; set; }
    }
}
