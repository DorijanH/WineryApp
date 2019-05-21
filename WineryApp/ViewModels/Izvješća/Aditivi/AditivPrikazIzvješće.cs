using System.ComponentModel.DataAnnotations;

namespace WineryApp.ViewModels.Izvješća.Podrumi
{
    public class AditivPrikazIzvješće
    {
        [Display(Name = "Ime aditiva")]
        public string ImeAditiva { get; set; }

        [Display(Name = "Vrsta aditiva")]
        public string VrstaAditiva { get; set; }

        public string Količina { get; set; }

        public string Koncentracija { get; set; }

        public string Instrukcije { get; set; }
    }
}
