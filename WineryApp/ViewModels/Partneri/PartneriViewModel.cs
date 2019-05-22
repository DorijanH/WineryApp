using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WineryApp.Data.Entiteti;

namespace WineryApp.ViewModels.Partneri
{
    public class PartneriViewModel
    {
        [BindProperty]
        public PartnerIM PartnerInput { get; set; }
        public List<Partner> Partneri { get; set; }
    }
}
