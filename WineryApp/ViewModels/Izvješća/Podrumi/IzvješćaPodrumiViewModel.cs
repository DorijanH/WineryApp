using System.Collections.Generic;
using WineryApp.Data.Entiteti;

namespace WineryApp.ViewModels.Izvješća.Podrumi
{
    public class IzvješćaPodrumiViewModel
    {
        public PodrumFilterIzvješće Input { get; set; }
        public List<Berba> Berbe { get; set; }
        public List<SortaVina> SorteVina { get; set; }
    }
}
