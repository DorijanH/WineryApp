using System.ComponentModel.DataAnnotations;

namespace WineryApp.ViewModels.Izvješća.Zadaci
{
    public class ZadatakPrikazIzvješće
    {
        public string Naziv { get; set; }

        public string Kategorija { get; set; }

        [Display(Name = "Šifra podruma")]
        public string ŠifraPodruma { get; set; }

        [Display(Name = "Šifra spremnika")]
        public string ŠifraSpremnika { get; set; }

        [Display(Name = "Početak zadatka")]
        public string PočetakZadatka { get; set; }

        [Display(Name = "Rok zadatka")]
        public string RokZadatka { get; set; }

        [Display(Name = "Zaduženi zaposlenik")]
        public string ZaduženiZaposlenik { get; set; }
       
    }
}
