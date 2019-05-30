using System.Collections.Generic;
using WineryApp.Data.Entiteti;

namespace WineryApp.ViewModels.Narudžbe
{
    public class NarudžbeViewModel
    {
        public NarudžbaIM NarudžbaInput { get; set; }
        public List<Narudžba> Narudžbe { get; set; }
        public NarudžbaFilter Filter { get; set; }
    }
}
