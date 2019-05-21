using System.ComponentModel.DataAnnotations;

namespace WineryApp.ViewModels.Izvješća.Spremnici
{
    public class RezultatAnalizePrikazIzvješće
    {
        [Display(Name = "Šifra uzorka")]
        public string ŠifraUzorka { get; set; }

        [Display(Name = "Datum analize")]
        public string DatumAnalize { get; set; }

        [Display(Name = "Šifra podruma")]
        public string ŠifraPodruma { get; set; }

        [Display(Name = "Šifra spremnika")]
        public string ŠifraSpremnika { get; set; }

        [Display(Name = "ph vrijednost")]
        public string PhVrijednost { get; set; }

        public string Šećer { get; set; }

        [Display(Name = "Rezidualni šećer")]
        public string RezidualniŠećer { get; set; }

        [Display(Name = "Slobodni sumpor")]
        public string SlobodniSumpor { get; set; }

        [Display(Name = "Ukupni sumpor")]
        public string UkupniSumpor { get; set; }

        public string Kiselina { get; set; }

        [Display(Name = "Postotak alkohola")]
        public string PostotakAlkohola { get; set; }

        [Display(Name = "Uzorak uzeo")]
        public string UzorakUzeo { get; set; }
    }
}
