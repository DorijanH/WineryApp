using System.ComponentModel.DataAnnotations;

namespace WineryApp.ViewModels.Izvješća.Spremnici
{
    public class SpremnikPrikazIzvješće
    {
        [Display(Name = "Šifra spremnika")]
        public string ŠifraSpremnika { get; set; }

        public string Napunjenost { get; set; }

        [Display(Name = "Faza procesa")]
        public string FazaProcesa { get; set; }

        [Display(Name = "Vrsta spremnika")]
        public string VrstaSpremnika { get; set; }

        [Display(Name = "Godina berbe")]
        public string GodinaBerbe { get; set; }

        public string Punilac { get; set; }

        [Display(Name = "Šifra podruma")]
        public string ŠifraPodruma { get; set; }

        [Display(Name = "Sorta vina")]
        public string SortaVina { get; set; }
    }
}
