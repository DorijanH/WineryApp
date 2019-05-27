using System.ComponentModel.DataAnnotations;

namespace WineryApp.ViewModels.Izvješća.Narudžbe
{
    public class NarudžbePrikazIzvješće
    {

        [Display(Name = "Ime kupca")]
        public string ImeKupca { get; set; }

        [Display(Name = "Adresa kupca")]
        public string AdresaKupca { get; set; }

        [Display(Name = "Status narudžbe")]
        public string StatusNarudžbe { get; set; }

        [Display(Name = "Datum narudžbe")]
        public string DatumNarudžbe { get; set; }

        [Display(Name = "Datum isporuke")]
        public string DatumIsporuke { get; set; }

        [Display(Name = "Datum naplate")]
        public string DatumNaplate { get; set; }

        [Display(Name = "Šifra spremnika")]
        public string ŠifraSpremnika { get; set; }

        public string Količina { get; set; }

        public string Cijena { get; set; }
    }
}
