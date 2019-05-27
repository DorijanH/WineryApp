using System.ComponentModel.DataAnnotations;

namespace WineryApp.ViewModels.Izvješća.Partneri
{
    public class PartnerPrikazIzvješće
    {
        public string Ime { get; set; }
        public string OIB { get; set; }

        [Display(Name = "Kontakt broj")]
        public string KontaktBroj { get; set; }
        public string Adresa { get; set; }

    }
}
