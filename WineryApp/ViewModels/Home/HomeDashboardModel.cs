using System.Collections.Generic;
using WineryApp.Data.Entiteti;

namespace WineryApp.ViewModels.Home
{
    public class HomeDashboardModel
    {
        public List<Zadatak> ZadaciDanas { get; set; }
        public List<PovijestSpremnika> PovijestSTjedanDana { get; set; }
        public Berba BerbaInput { get; set; }
    }
}
