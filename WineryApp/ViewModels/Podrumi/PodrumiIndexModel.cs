using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WineryApp.Data.Entiteti;

namespace WineryApp.ViewModels.Podrumi
{
    public class PodrumiIndexModel
    {
        [BindProperty]
        public PodrumIM PodrumInput { get; set; }
        public List<Podrum> Podrumi { get; set; }
        public PodrumiFilter Filter { get; set; }
    }
}
