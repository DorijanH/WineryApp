using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WineryApp.Data.Entiteti;

namespace WineryApp.ViewModels.Aditivi
{
    public class AditiviViewModel
    {
        [BindProperty]
        public AditiviIM AditivInput { get; set; }
        public List<Aditiv> Aditivi { get; set; }
        public AditivFilter Filter { get; set; }
    }
}
