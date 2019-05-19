using System.ComponentModel.DataAnnotations;

namespace WineryApp.ViewModels.Izvješća.Podrumi
{
    public class PodrumPrikazIzvješće
    {
        [Display(Name = "Šifra podruma")]
        public string ŠifraPodruma { get; set; }

        public string Popunjenost { get; set; }

        [Display(Name = "Broj spremnika")]
        public string BrojSpremnika { get; set; }

        public string Lokacija { get; set; }

        public string Berbe { get; set; }

        [Display(Name = "Sorte vina")]
        public string SorteVina { get; set; }
    }
}
